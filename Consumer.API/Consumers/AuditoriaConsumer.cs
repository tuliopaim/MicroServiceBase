using Confluent.Kafka;
using CQRS.Core.API;
using CQRS.Core.Infrastructure.Kafka;
using CQRS.Core.Infrastructure.Kafka.KafkaEventTypes;
using System.Threading;
using System.Threading.Tasks;

namespace AuditoriaAPI.Consumers
{
    public class AuditoriaConsumer : IConsumerHandler
    {
        private readonly IConsumer<string, string> _consumer;

        public AuditoriaConsumer(IKafkaBroker kafkaBroker)
        {
            _consumer = kafkaBroker.GetConsumer(KafkaTopics.AuditoriaTopic);

        }

        public async Task Handle(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var message = _consumer.GetMessage<PessoaEventTypes>(cancellationToken);



            }
        }
    }
}
