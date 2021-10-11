using System.Threading;
using System.Threading.Tasks;
using CQRS.Core.Application;
using CQRS.Core.Infrastructure.Kafka;

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
            await _broker.PublishAsync(KafkaTopics.NovaPessoaTopic, (int)KafkaEventTypes.PessoaCriada, @event, cancellationToken);
        }
    }
}