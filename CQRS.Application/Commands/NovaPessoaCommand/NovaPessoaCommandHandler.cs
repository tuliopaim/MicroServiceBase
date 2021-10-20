using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Application.Events.PessoaCriadaEvent;
using CQRS.Core.Application.Commands;
using CQRS.Core.Application.Mediator;
using CQRS.Domain.Entities;
using CQRS.Domain.Repositories;

namespace CQRS.Application.Commands.NovaPessoaCommand
{
    public class NovaPessoaCommandHandler : ICommandHandler<NovaPessoaCommandInput, NovaPessoaCommandResult>
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IMediator _mediator;

        public NovaPessoaCommandHandler(IPessoaRepository pessoaRepository, IMediator mediator)
        {
            _pessoaRepository = pessoaRepository;
            _mediator = mediator;
        }

        public async Task<NovaPessoaCommandResult> Handle(NovaPessoaCommandInput command, CancellationToken cancellationToken)
        {
            var pessoa = new Pessoa(command.Nome, command.Idade);

            _pessoaRepository.Add(pessoa);

            await _pessoaRepository.UnitOfWork.CommitAsync(cancellationToken);

            var novaPessoaEvent = new PessoaCriadaEventInput(pessoa.Id);

            await _mediator.Publish(novaPessoaEvent, cancellationToken);

            var result = new NovaPessoaCommandResult(pessoa.Id).WithHttpStatusCode(HttpStatusCode.Created);
            
            return result as NovaPessoaCommandResult;
        }
    }
}