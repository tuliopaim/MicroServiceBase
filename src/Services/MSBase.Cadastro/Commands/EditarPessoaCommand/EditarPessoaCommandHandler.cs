using MSBase.Cadastro.API.Infrastructure.Repositories;
using MSBase.Core.Application.Commands;

namespace MSBase.Cadastro.API.Commands.EditarPessoaCommand;

public class EditarPessoaCommandHandler : ICommandHandler<EditarPessoaCommandInput, EditarPessoaCommandResult>
{
    private readonly IPessoaRepository _pessoaRepository;

    public EditarPessoaCommandHandler(
        IPessoaRepository pessoaRepository)
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

            return null;
        }

        pessoa.AlterarIdade(command.NovaIdade);

        await _pessoaRepository.UnitOfWork.CommitAsync(cancellationToken);

        return new EditarPessoaCommandResult();
    }
}