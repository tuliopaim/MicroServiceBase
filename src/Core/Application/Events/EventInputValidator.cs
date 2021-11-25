using Core.Application.Mediator;

namespace Core.Application.Events
{
    public class EventInputValidator<TEventInput> : MediatorInputValidator<TEventInput> where TEventInput : EventInput
    {
    }
}