using MediatR;

namespace MSBase.Core.Cqrs.Events;

public interface IEventHandler<in TEventInput> : INotificationHandler<TEventInput> where TEventInput : EventInput
{
    new Task Handle(TEventInput @event, CancellationToken cancellationToken);
}