using MSBase.Core.Infrastructure.Kafka;

namespace Cadastro.API.Consumers.NovaPessoaConsumer
{
    public class NovaPessoaConsumerInput : IKafkaEvent
    {
        public NovaPessoaConsumerInput(Guid pessoaId)
        {
            PessoaId = pessoaId;
        }

        public Guid PessoaId { get; private set; }
    }
}   