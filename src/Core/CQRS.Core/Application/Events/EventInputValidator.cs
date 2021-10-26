using CQRS.Core.Application.Mediator;

namespace CQRS.Core.Application.Events
{
    public class EventInputValidator<TEventInput> : MediatorInputValidator<TEventInput> where TEventInput : EventInput
    {
    }
}