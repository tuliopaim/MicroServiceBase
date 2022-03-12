using FluentValidation;

namespace MSBase.Core.Application.Mediator;

public class MediatorValidator<TMediatorInput> : AbstractValidator<TMediatorInput>, IMediatorValidator
{
}