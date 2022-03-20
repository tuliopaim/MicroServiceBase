namespace MSBase.Core.Cqrs.Queries;

public class PagedQueryResult<TResult> : ListQueryResult<TResult>
{
    public QueryPagination Pagination { get; set; } = new();
}