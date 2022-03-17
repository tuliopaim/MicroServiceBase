using MSBase.Auditoria.API.Commands.NovaAuditoriaCommand;
using MSBase.Core.Cqrs.Mediator;
using MSBase.Core.Extensions;
using MSBase.Core.RabbitMq;
using MSBase.Core.RabbitMq.Messages;
using MSBase.Core.RabbitMq.Messages.Auditoria;
using RabbitMQ.Client.Events;

namespace MSBase.Auditoria.API.Consumers;

public class AuditoriaConsumerBackgroundService : RabbitMqConsumerBackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public AuditoriaConsumerBackgroundService(
        RabbitMqConnection rabbitMqConnection,
        ILogger<AuditoriaConsumerBackgroundService> logger,
        IServiceProvider serviceProvider) : base(rabbitMqConnection, logger, RoutingKeys.NovaAuditoria)
    {
        _serviceProvider = serviceProvider;
    }
    
    protected override async Task<bool> HandleMessage(BasicDeliverEventArgs rabbitEventArgs, RabbitMessage message)
    {
        var mensagem = rabbitEventArgs.GetDeserializedMessage<AuditoriaMessage>();

        using var scope = _serviceProvider.CreateScope();
        var mediator = scope.ServiceProvider.GetService<IMediator>();

        var command = MapearParaCommand(mensagem);

        var result = await mediator!.Send(command);
        
        return result.IsValid();
    }
    
    private static NovaAuditoriaCommandInput MapearParaCommand(AuditoriaMessage mensagem)
    {
        return new NovaAuditoriaCommandInput
        {
            Auditorias = mensagem.Auditorias.Select(a =>
                new NovaAuditoriaCommandInputItem(
                    a.TipoAuditoria,
                    a.IdEntidade,
                    a.NomeEntidade,
                    a.NomeTabela,
                    a.Propriedades.Select(p => new NovaAuditoriaPropriedadeCommandInputItem(
                        p.NomeDaPropriedade,
                        p.NomeDaColuna,
                        p.ValorAntigo,
                        p.ValorNovo,
                        p.EhChavePrimaria)).ToList()))
        };
    }
}