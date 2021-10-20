using CQRS.Core.Application.Events;
using FluentValidation;

namespace CQRS.Application.Events.PessoaCriadaEvent
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