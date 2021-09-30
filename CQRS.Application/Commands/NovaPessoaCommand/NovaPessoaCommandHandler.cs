using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Core.Application.Interfaces;

namespace CQRS.Application.Commands.NovaPessoaCommand
{
    public class NovaPessoaCommandHandler : ICommandHandler<NovaPessoaCommandInput, NovaPessoaCommandResult>
    {
        public Task<NovaPessoaCommandResult> Handle(NovaPessoaCommandInput command, CancellationToken cancellationToken)
        {
            var result = new NovaPessoaCommandResult
            {
                Id = Guid.NewGuid()
            }.WithHttpStatusCode(HttpStatusCode.Created);

            return Task.FromResult(result as NovaPessoaCommandResult);
        }
    }
}