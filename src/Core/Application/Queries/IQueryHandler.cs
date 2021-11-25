using Core.Application.Mediator;
using MediatR;

namespace Core.Application.Queries
{
    public interface IQueryHandler<in TQueryInput, TQueryResult> : IMediatorHandler<TQueryInput, TQueryResult>
        where TQueryInput : IRequest<TQueryResult>, IMediatorInput<TQueryResult>
        where TQueryResult : IMediatorResult
    {
        new Task<TQueryResult> Handle(TQueryInput query, CancellationToken cancellationToken);
    }
}