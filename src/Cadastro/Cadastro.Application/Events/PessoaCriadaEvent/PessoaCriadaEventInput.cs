using System;
using MSBase.Core.Application.Mediator;
using MSBase.Core.Infrastructure.Kafka;

namespace Cadastro.Application.Events.PessoaCriadaEvent
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