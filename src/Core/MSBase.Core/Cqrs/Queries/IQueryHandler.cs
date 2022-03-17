using MediatR;
using MSBase.Core.Cqrs.Mediator;

namespace MSBase.Core.Cqrs.Queries;

public interface IQueryHandler<in TQueryInput, TQueryResult> : IMediatorHandler<TQueryInput, TQueryResult>
    where TQueryInput : IRequest<TQueryResult>, IMediatorInput<TQueryResult>
    where TQueryResult : IMediatorResult
{
    new Task<TQueryResult> Handle(TQueryInput query, CancellationToken cancellationToken);
}