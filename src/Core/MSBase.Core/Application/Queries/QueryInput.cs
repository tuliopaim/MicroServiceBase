using MSBase.Core.Application.Mediator;

namespace MSBase.Core.Application.Queries;

public class QueryInput<TQueryResult> : MediatorInput<TQueryResult>
    where TQueryResult : QueryResult
{
}