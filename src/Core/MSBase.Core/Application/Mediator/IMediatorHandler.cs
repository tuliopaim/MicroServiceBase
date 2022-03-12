using MediatR;

namespace MSBase.Core.Application.Mediator;

public interface IMediatorHandler<in TMediatorInput, TMediatorResult> : IRequestHandler<TMediatorInput, TMediatorResult>
    where TMediatorInput : IRequest<TMediatorResult>, IMediatorInput<TMediatorResult>
    where TMediatorResult : IMediatorResult
{
    new Task<TMediatorResult> Handle(TMediatorInput request, CancellationToken cancellationToken);
}