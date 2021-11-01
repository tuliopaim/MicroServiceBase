using FluentValidation;
using MSBase.Core.Application.Events;

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