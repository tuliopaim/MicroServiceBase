using System;
using CQRS.Core.Application;

namespace CQRS.Application.Events.PessoaCriadaEvent
{
    public class PessoaCriadaEventInput : EventInput
    {
        public PessoaCriadaEventInput(Guid pessoaId)
        {
            PessoaId = pessoaId;
        }

        public Guid PessoaId { get; private set; }
    }
}