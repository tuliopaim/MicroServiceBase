using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using CQRS.Core.API;
using CQRS.Core.Infrastructure.Kafka;
using CQRS.Core.Infrastructure.Kafka.KafkaEventTypes;
using Microsoft.Extensions.Logging;

namespace Cadastro.API.Consumers.NovaPessoaConsumer
{
    public class NovaPessoaConsumer : IConsumerHandler
    {
        private readonly IConsumer<string, string> _consumer;
        private readonly ILogger<NovaPessoaConsumer> _logger;

        public NovaPessoaConsumer(IKafkaBroker kafkaBroker, ILogger<NovaPessoaConsumer> logger)
        {
            _consumer = kafkaBroker.GetConsumer(KafkaTopics.NovaPessoaTopic);
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