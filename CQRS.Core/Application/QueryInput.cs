namespace CQRS.Core.Application
{
    public class QueryInput<TQueryResult> : MediatorInput<TQueryResult>
        where TQueryResult : QueryResult
    {
    }
}