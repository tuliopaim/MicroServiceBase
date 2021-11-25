using Confluent.Kafka;
using Core.API;
using Core.Infrastructure.Kafka;
using Core.Infrastructure.Kafka.KafkaMessageTypes;

namespace Cadastro.API.Consumers.NovaPessoaConsumer
{
    public class NovaPessoaConsumer : IConsumerHandler
    {
        private readonly IConsumer<string, string> _consumer;
        private readonly ILogger<NovaPessoaConsumer> _logger;

        public NovaPessoaConsumer(IKafkaBroker kafkaBroker, ILogger<NovaPessoaConsumer> logger)
        {
            _consumer = kafkaBroker.GetConsumer(KafkaTopics.NovaPessoa);
            _logger = logger;
        }

        public async Task Handle(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var message = _consumer.GetMessage<PessoaMessageTypes>(cancellationToken);

                var handleTask = message.EventType switch
                {
                    PessoaMessageTypes.PessoaCriada => HandlePessoaCriada(message),
                    _ => Task.CompletedTask
                };

                await handleTask;
            }
        }

        private Task HandlePessoaCriada(PlatformMessage<PessoaMessageTypes> platformMessage)
        {
            var @event = platformMessage.GetEvent<NovaPessoaConsumerInput, PessoaMessageTypes>();
            
            _logger.LogDebug("{ConsumerInputType} - Recebido.", nameof(NovaPessoaConsumerInput));

            return Task.CompletedTask;
        }
    }
}