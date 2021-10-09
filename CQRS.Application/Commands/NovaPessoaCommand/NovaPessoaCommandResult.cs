using System;
using CQRS.Core.Application;

namespace CQRS.Application.Commands.NovaPessoaCommand
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