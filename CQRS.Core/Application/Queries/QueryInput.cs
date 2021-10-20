using CQRS.Core.Application.Mediator;

namespace CQRS.Core.Application.Queries
{
    public class QueryInput<TQueryResult> : MediatorInput<TQueryResult>
        where TQueryResult : QueryResult
    {
    }
}