using Core.Application.Mediator;

namespace Core.Application.Queries
{
    public class QueryInput<TQueryResult> : MediatorInput<TQueryResult>
        where TQueryResult : QueryResult
    {
    }
}