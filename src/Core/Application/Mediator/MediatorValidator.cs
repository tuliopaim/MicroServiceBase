using FluentValidation;

namespace Core.Application.Mediator
{
    public class MediatorValidator<TMediatorInput> : AbstractValidator<TMediatorInput>, IMediatorValidator
    {
    }
}