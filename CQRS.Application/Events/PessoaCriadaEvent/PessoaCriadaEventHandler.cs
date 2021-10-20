using System.Threading;
using System.Threading.Tasks;
using CQRS.Core.Application.Events;
using CQRS.Core.Infrastructure.Kafka;
using CQRS.Core.Infrastructure.Kafka.KafkaEventTypes;

namespace CQRS.Application.Events.PessoaCriadaEvent
{
    public class PessoaCriadaEventHandler : IEventHandler<PessoaCriadaEventInput>
    {
        private readonly IKafkaBroker _broker;

        public PessoaCriadaEventHandler(IKafkaBroker broker)
        {
            _broker = broker;
        }

        public async Task Handle(PessoaCriadaEventInput @event, CancellationToken cancellationToken)
        {
            await _broker.PublishAsync(KafkaTopics.NovaPessoaTopic, PessoaEventTypes.PessoaCriada, @event, cancellationToken);
        }
    }
}