namespace MSBase.Core.Cqrs.Queries;

public class ListQueryResult<TResult> : QueryResult
{
    public IEnumerable<TResult> Results { get; set; } = new List<TResult>();
}