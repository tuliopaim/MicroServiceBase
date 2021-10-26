using System.Threading;
using System.Threading.Tasks;
using CQRS.Core.Application.Mediator;
using MediatR;

namespace CQRS.Core.Application.Events
{
    public interface IEventHandler<in TEventInput> : INotificationHandler<TEventInput> where TEventInput : EventInput
    {
        new Task Handle(TEventInput @event, CancellationToken cancellationToken);
    }
}