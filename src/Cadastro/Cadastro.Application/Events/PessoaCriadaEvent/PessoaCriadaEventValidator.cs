using Core.Application.Events;
using FluentValidation;

namespace Cadastro.Application.Events.PessoaCriadaEvent
{
    public class PessoaCriadaEventValidator : EventInputValidator<PessoaCriadaEventInput>
    {
        public PessoaCriadaEventValidator()
        {
            RuleFor(x => x.PessoaId)
                .NotEmpty();
        }
    }
}