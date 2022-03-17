using MSBase.Core.Cqrs.Mediator;

namespace MSBase.Core.Cqrs.Queries;

public class QueryInput<TQueryResult> : MediatorInput<TQueryResult>
    where TQueryResult : QueryResult
{
}