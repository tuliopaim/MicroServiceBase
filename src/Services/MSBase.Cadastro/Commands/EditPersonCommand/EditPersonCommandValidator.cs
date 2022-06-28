using EasyCqrs.Commands;
using FluentValidation;

namespace MSBase.Cadastro.API.Commands.EditPersonCommand;

public class EditPersonCommandValidator : CommandInputValidator<EditPersonCommandInput>
{
    public EditPersonCommandValidator()
    {
        RuleFor(x => x.PessoaId).NotEmpty();

        RuleFor(x => x.NovaIdade).GreaterThanOrEqualTo((byte)18);
    }
}