using System;
using CQRS.Core.Application.Commands;

namespace Cadastro.Application.Commands.NovaPessoaCommand
{
    public class NovaPessoaCommandResult : CommandResult
    {
        public NovaPessoaCommandResult(Guid id)
        {
            Id = id;
        }
        
        public Guid Id { get; }
    }
}