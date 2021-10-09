using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Core.Application;
using CQRS.Domain.Entities;
using CQRS.Domain.Repositories;

namespace CQRS.Application.Commands.NovaPessoaCommand
{
    public class NovaPessoaCommandHandler : ICommandHandler<NovaPessoaCommandInput, NovaPessoaCommandResult>
    {
        private readonly IPessoaRepository _pessoaRepository;

        public NovaPessoaCommandHandler(IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        public async Task<NovaPessoaCommandResult> Handle(NovaPessoaCommandInput command, CancellationToken cancellationToken)
        {
            var pessoa = new Pessoa(command.Nome, command.Idade);

            _pessoaRepository.Add(pessoa);

            await _pessoaRepository.UnitOfWork.CommitAsync(cancellationToken);

            var result = new NovaPessoaCommandResult(pessoa.Id).WithHttpStatusCode(HttpStatusCode.Created);
            
            return result as NovaPessoaCommandResult;
        }
    }
}