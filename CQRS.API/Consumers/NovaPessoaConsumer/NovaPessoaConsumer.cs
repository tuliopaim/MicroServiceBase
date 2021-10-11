using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using CQRS.Core.API;
using CQRS.Core.Infrastructure.Kafka;
using CQRS.Core.Infrastructure.Kafka.KafkaEventTypes;

namespace CQRS.API.Consumers.NovaPessoaConsumer
{
    public class NovaPessoaConsumer : IConsumerHandler
    {
        private readonly IConsumer<string, string> _consumer;

        public NovaPessoaConsumer(IKafkaBroker kafkaBroker)
        {
            _consumer = kafkaBroker.GetConsumer(KafkaTopics.NovaPessoaTopic);
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

        private static Task HandlePessoaCriada(PlatformMessage<PessoaEventTypes> platformMessage)
        {
            var @event = platformMessage.GetEvent<NovaPessoaConsumerInput, PessoaEventTypes>();
            
            Console.WriteLine($"Evento do {nameof(NovaPessoaConsumer)} recebido - PessoaId: [{@event.PessoaId}]");

            return Task.CompletedTask;
        }
    }
}