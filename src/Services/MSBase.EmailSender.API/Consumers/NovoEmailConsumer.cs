using MSBase.Core.Infrastructure.RabbitMq.Messages.Email;
using MSBase.EmailSender.API.Domain;

namespace MSBase.EmailSender.API.Consumers;

public class NovoEmailConsumer : IConsumerHandler
{
    private readonly IConsumer<string, string> _consumer;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<NovoEmailConsumer> _logger;

    public NovoEmailConsumer(
        IServiceProvider serviceProvider,
        ILogger<NovoEmailConsumer> logger,
        IKafkaBroker kafkaBroker)
    {
        _consumer = kafkaBroker.GetConsumer(KafkaTopics.NovoEmail);
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task Handle(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var platformMessage = _consumer.GetMessage<EmailMessageTypes>(cancellationToken);

            var handleTask = platformMessage.EventType switch
            {
                EmailMessageTypes.EmailPessoaCadastradaComSucesso => HandlePessoaCadastradaComSucesso(platformMessage),
                _ => Task.CompletedTask
            };

            await handleTask;
        }
    }

    private async Task HandlePessoaCadastradaComSucesso(PlatformMessage<EmailMessageTypes> platformMessage)
    {
        var message = platformMessage.GetMessage<EmailPessoaCadastradaComSucessoMessage, EmailMessageTypes>();

        using var scope = _serviceProvider.CreateScope();

        var emailService = scope.ServiceProvider.GetService<IEmailService>();

        ArgumentNullException.ThrowIfNull(emailService, nameof(emailService));

        await emailService.EnviarEmailPessoaCadastradaComSucesso(message.NomePessoa, message.Destinatario);
    }
}