using Core.Application.Mediator;
using MediatR;

namespace Core.Application.Events
{
    public interface IEventHandler<in TEventInput> : INotificationHandler<TEventInput> where TEventInput : EventInput
    {
        new Task Handle(TEventInput @event, CancellationToken cancellationToken);
    }
}