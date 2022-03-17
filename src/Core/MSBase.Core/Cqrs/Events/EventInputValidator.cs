using MSBase.Core.Cqrs.Mediator;

namespace MSBase.Core.Cqrs.Events;

public class EventInputValidator<TEventInput> : MediatorInputValidator<TEventInput> where TEventInput : EventInput
{
}