using System;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Core.Application.Interfaces;

namespace CQRS.Application.Commands.NovaPessoaCommand
{
    public class NovaPessoaCommandHandler : ICommandHandler<NovaPessoaCommandInput, NovaPessoaCommandResult>
    {
        public Task<NovaPessoaCommandResult> Handle(NovaPessoaCommandInput command, CancellationToken cancellationToken)
        {
            return Task.FromResult(new NovaPessoaCommandResult
            {
                Id = Guid.NewGuid()
            });
        }
    }
}