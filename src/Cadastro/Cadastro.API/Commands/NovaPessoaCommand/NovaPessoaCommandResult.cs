using Core.Application.Commands;

namespace Cadastro.API.Commands.NovaPessoaCommand
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