using System;
using CQRS.Core.Application;
using CQRS.Core.Infrastructure.Kafka;

namespace CQRS.API.Consumers.NovaPessoaConsumer
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