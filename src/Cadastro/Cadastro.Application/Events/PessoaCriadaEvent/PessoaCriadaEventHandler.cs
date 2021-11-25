using MSBase.Core.Application.Events;
using MSBase.Core.Infrastructure.Kafka;
using MSBase.Core.Infrastructure.Kafka.KafkaEventTypes;

namespace Cadastro.Application.Events.PessoaCriadaEvent
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
            await _broker.PublishAsync(KafkaTopics.NovaPessoa, PessoaEventTypes.PessoaCriada, @event, cancellationToken);
        }
    }
}