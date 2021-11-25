using Core.Application.Events;
using Core.Infrastructure.Kafka;
using Core.Infrastructure.Kafka.KafkaMessageTypes;

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
            await _broker.PublishAsync(KafkaTopics.NovaPessoa, PessoaMessageTypes.PessoaCriada, @event, cancellationToken);
        }
    }
}