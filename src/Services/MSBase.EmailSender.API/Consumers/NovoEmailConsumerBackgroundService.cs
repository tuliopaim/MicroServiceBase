using MSBase.Core.Extensions;
using MSBase.Core.RabbitMq;
using MSBase.Core.RabbitMq.Messages;
using MSBase.Core.RabbitMq.Messages.Email;
using MSBase.EmailSender.API.Domain;
using RabbitMQ.Client.Events;

namespace MSBase.EmailSender.API.Consumers;

public class NovoEmailConsumerBackgroundService : RabbitMqConsumerBackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<NovoEmailConsumerBackgroundService> _logger;

    public NovoEmailConsumerBackgroundService(
        RabbitMqConnection rabbitMqConnection,
        ILogger<NovoEmailConsumerBackgroundService> logger,
        IServiceProvider serviceProvider) : base(rabbitMqConnection, logger, RoutingKeys.NovoEmail)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }
    
    protected override async Task<bool> HandleMessage(BasicDeliverEventArgs rabbitEventArgs, RabbitMessage message)
    {
        var mensagem = rabbitEventArgs.GetRabbitMessage();

        var task = mensagem.MessageType switch
        {
            MessageType.EmailPessoaCadastradaComSucesso => HandlePessoaCadastradaComSucesso(mensagem),
            _ => Task.FromResult(true)
        };
        
        return await task;
    }
    
    private async Task<bool> HandlePessoaCadastradaComSucesso(RabbitMessage rabbitMessage)
    {
        var mensagem = rabbitMessage.GetDeserializedMessage<EmailPessoaCadastradaComSucessoMessage>();

        using var scope = _serviceProvider.CreateScope();
        var emailService = scope.ServiceProvider.GetService<IEmailService>();

        ArgumentNullException.ThrowIfNull(emailService, nameof(emailService));

        return await emailService.EnviarEmailPessoaCadastradaComSucesso(mensagem.NomePessoa, mensagem.Destinatario);
    }

}