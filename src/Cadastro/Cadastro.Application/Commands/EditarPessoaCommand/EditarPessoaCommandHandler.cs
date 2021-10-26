using CQRS.Core.Application.Commands;
using CQRS.Domain.Repositories;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Application.Commands.EditarPessoaCommand
{
    public class EditarPessoaCommandHandler : ICommandHandler<EditarPessoaCommandInput, EditarPessoaCommandResult>
    {
        private readonly IPessoaRepository _pessoaRepository;

        public EditarPessoaCommandHandler(IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        public async Task<EditarPessoaCommandResult> Handle(EditarPessoaCommandInput command, CancellationToken cancellationToken)
        {
            var pessoa = await _pessoaRepository.ObterPorId(command.PessoaId);

            if (pessoa is null)
            {
                var result = new EditarPessoaCommandResult();
                result.AddError("Pessoa não encontrada");

                return (EditarPessoaCommandResult) result.WithHttpStatusCode(HttpStatusCode.NotFound); 
            }

            pessoa.AlterarIdade(command.NovaIdade);

            await _pessoaRepository.UnitOfWork.CommitAsync(cancellationToken);

            return new EditarPessoaCommandResult();
        }
    }
}
