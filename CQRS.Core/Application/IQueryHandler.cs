using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace CQRS.Core.Application
{
    public interface IQueryHandler<in TQueryInput, TQueryResult>
        : IMediatorHandler<TQueryInput, TQueryResult>
        where TQueryInput : IRequest<TQueryResult>, IMediatorInput<TQueryResult>
        where TQueryResult : IMediatorResult
    {
        new Task<TQueryResult> Handle(TQueryInput query, CancellationToken cancellationToken);
    }
}