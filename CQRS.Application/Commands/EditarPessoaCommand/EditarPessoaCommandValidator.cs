using CQRS.Core.Application.Commands;
using FluentValidation;

namespace CQRS.Application.Commands.EditarPessoaCommand
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
