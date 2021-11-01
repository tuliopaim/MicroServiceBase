using FluentValidation;
using MSBase.Core.Application.Commands;

namespace Cadastro.Application.Commands.EditarPessoaCommand
{
    public class EditarPessoaCommandValidator : CommandInputValidator<EditarPessoaCommandInput>
    {
        public EditarPessoaCommandValidator()
        {
            RuleFor(x => x.PessoaId).NotEmpty();

            RuleFor(x => x.NovaIdade).GreaterThanOrEqualTo((byte)18);
        }
    }
}
