namespace MSBase.Core.Cqrs.Queries;

public class PagedQueryResult<TResultItem> : QueryResult
{
    public IEnumerable<TResultItem> Result { get; set; }
    public QueryPaginationResult Pagination { get; set; }
}