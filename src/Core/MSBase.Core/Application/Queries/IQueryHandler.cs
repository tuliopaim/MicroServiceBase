using MediatR;
using MSBase.Core.Application.Mediator;

namespace MSBase.Core.Application.Queries
{
    public interface IQueryHandler<in TQueryInput, TQueryResult> : IMediatorHandler<TQueryInput, TQueryResult>
        where TQueryInput : IRequest<TQueryResult>, IMediatorInput<TQueryResult>
        where TQueryResult : IMediatorResult
    {
        new Task<TQueryResult> Handle(TQueryInput query, CancellationToken cancellationToken);
    }
}