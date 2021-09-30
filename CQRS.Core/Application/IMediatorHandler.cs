using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace CQRS.Core.Application
{
    public interface IMediatorHandler<in TMediatorInput, TMediatorResult> : IRequestHandler<TMediatorInput, TMediatorResult>
        where TMediatorInput : IRequest<TMediatorResult>, IMediatorInput<TMediatorResult>
        where TMediatorResult : IMediatorResult
    {
        new Task<IMediatorResult> Handle(TMediatorInput request, CancellationToken cancellationToken);
    }
}