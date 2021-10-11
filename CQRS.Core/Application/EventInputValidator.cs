namespace CQRS.Core.Application
{
    public class EventInputValidator<TEventInput> : MediatorInputValidator<TEventInput> where TEventInput : EventInput
    {
    }
}