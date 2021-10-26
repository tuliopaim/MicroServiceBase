using CQRS.Core.Application.Commands;
using System;

namespace CQRS.Application.Commands.EditarPessoaCommand
{
    public class EditarPessoaCommandInput : CommandInput<EditarPessoaCommandResult>
    {        
        public EditarPessoaCommandInput(Guid pessoaId, byte novaIdade)
        {
            PessoaId = pessoaId;
            NovaIdade = novaIdade;
        }

        public Guid PessoaId { get; }
        public byte NovaIdade { get; }
    }
}
