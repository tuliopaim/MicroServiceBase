using System;
using CQRS.Core.Application;
using CQRS.Core.Infrastructure.Kafka;

namespace CQRS.Application.Events.PessoaCriadaEvent
{
    public class PessoaCriadaEventInput : EventInput, IKafkaEvent
    {
        public PessoaCriadaEventInput(Guid pessoaId)
        {
            PessoaId = pessoaId;
        }

        public Guid PessoaId { get; private set; }
    }
}