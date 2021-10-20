using FluentValidation;

namespace CQRS.Core.Application.Mediator
{
    public class MediatorValidator<TMediatorInput> : AbstractValidator<TMediatorInput>, IMediatorValidator
    {
    }
}