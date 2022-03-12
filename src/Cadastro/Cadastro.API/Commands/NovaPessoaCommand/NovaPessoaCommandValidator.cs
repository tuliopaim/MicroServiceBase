using Core.Application.Commands;
using FluentValidation;

namespace Cadastro.API.Commands.NovaPessoaCommand
{
    public class NovaPessoaCommandValidator : CommandInputValidator<NovaPessoaCommandInput>
    {
        public NovaPessoaCommandValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .MaximumLength(40);

            RuleFor(x => x.Idade)
                .GreaterThanOrEqualTo((byte)18);
        }
    }
}