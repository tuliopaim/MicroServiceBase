using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MSBase.Core.Application.Mediator;

namespace MSBase.Core.Application.Events
{
    public interface IEventHandler<in TEventInput> : INotificationHandler<TEventInput> where TEventInput : EventInput
    {
        new Task Handle(TEventInput @event, CancellationToken cancellationToken);
    }
}