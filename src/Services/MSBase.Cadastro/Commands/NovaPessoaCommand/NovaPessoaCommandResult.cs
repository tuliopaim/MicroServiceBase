using MSBase.Core.Cqrs.Commands;

namespace MSBase.Cadastro.API.Commands.NovaPessoaCommand;

public class NovaPessoaCommandResult : CommandResult
{
    public NovaPessoaCommandResult(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}