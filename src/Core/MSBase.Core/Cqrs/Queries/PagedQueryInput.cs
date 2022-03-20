namespace MSBase.Core.Cqrs.Queries;

public class PagedQueryInput<TQueryResult>
    : QueryInput<TQueryResult> where TQueryResult : QueryResult
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}