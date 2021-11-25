using Confluent.Kafka;
using MSBase.Core.API;
using MSBase.Core.Infrastructure.Kafka;
using MSBase.Core.Infrastructure.Kafka.KafkaEventTypes;

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
                var message = _consumer.GetMessage<PessoaEventTypes>(cancellationToken);

                var handleTask = message.EventType switch
                {
                    PessoaEventTypes.PessoaCriada => HandlePessoaCriada(message),
                    _ => Task.CompletedTask
                };

                await handleTask;
            }
        }

        private Task HandlePessoaCriada(PlatformMessage<PessoaEventTypes> platformMessage)
        {
            var @event = platformMessage.GetEvent<NovaPessoaConsumerInput, PessoaEventTypes>();
            
            _logger.LogDebug("{ConsumerInputType} - Recebido.", nameof(NovaPessoaConsumerInput));

            return Task.CompletedTask;
        }
    }
}