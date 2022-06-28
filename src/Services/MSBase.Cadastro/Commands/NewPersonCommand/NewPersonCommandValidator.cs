using EasyCqrs.Commands;
using FluentValidation;

namespace MSBase.Cadastro.API.Commands.NewPersonCommand;

public class NewPersonCommandValidator : CommandInputValidator<NewPersonCommandInput>
{
    public NewPersonCommandValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
            .MaximumLength(40);

        RuleFor(x => x.Idade)
            .GreaterThanOrEqualTo((byte)18);
    }
}