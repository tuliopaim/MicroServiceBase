using CQRS.Core.Application.Interfaces;
using FluentValidation;

namespace CQRS.Core.Application
{
    public class MediatorValidator<TMediatorInput> : AbstractValidator<TMediatorInput>, IMediatorValidator
    {
    }
}