using System.Text.Json;
using Auditoria.API.Application.NovaAuditoriaCommand;
using Confluent.Kafka;
using Core.API;
using Core.Application.Mediator;
using Core.Infrastructure.Auditoria;
using Core.Infrastructure.Kafka;
using Core.Infrastructure.Kafka.KafkaMessageTypes;

namespace Auditoria.API.Consumers
{
    public class AuditoriaConsumer : IConsumerHandler
    {
        private readonly IConsumer<string, string> _consumer;
        private readonly IServiceProvider _serviceProvider;

        public AuditoriaConsumer(IKafkaBroker kafkaBroker, IServiceProvider serviceProvider)
        {
            _consumer = kafkaBroker.GetConsumer(KafkaTopics.NovaAuditoria);
            this._serviceProvider = serviceProvider;
        }

        public async Task Handle(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var platformMessage = _consumer.GetMessage<AuditoriaMessageTypes>(cancellationToken);

                using var scope = _serviceProvider.CreateScope();

                var mediator = scope.ServiceProvider.GetService<IMediator>();

                var message = JsonSerializer.Deserialize<AuditoriaMessage>(platformMessage.Data);

                var command = MapearParaCommand(message);
                
                var result = await mediator!.Send(command, cancellationToken);
            }
        }

        private static NovaAuditoriaCommandInput MapearParaCommand(AuditoriaMessage @event)
        {
            return new NovaAuditoriaCommandInput
            {
                Auditorias = @event.Auditorias.Select(a =>
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
}
