using System;
using CQRS.Core.Application;

namespace CQRS.Application.Commands.NovaPessoaCommand
{
    public class NovaPessoaCommandResult : CommandResult
    {
        public Guid Id { get; set; }
    }
}