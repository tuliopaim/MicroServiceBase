using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace CQRS.Core.Application.Interfaces
{
    public interface IEventHandler<in TEventInput> : INotificationHandler<TEventInput> where TEventInput : EventInput
    {
        new Task Handle(TEventInput @event, CancellationToken cancellationToken);
    }
}