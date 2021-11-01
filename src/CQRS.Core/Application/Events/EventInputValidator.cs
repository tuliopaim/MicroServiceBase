using MSBase.Core.Application.Mediator;

namespace MSBase.Core.Application.Events
{
    public class EventInputValidator<TEventInput> : MediatorInputValidator<TEventInput> where TEventInput : EventInput
    {
    }
}