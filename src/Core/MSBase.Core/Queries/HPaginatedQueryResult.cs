using EasyCqrs.Queries;

namespace MSBase.Core.Queries;

public class HPaginatedQueryResult<TResult> : QueryPaginatedResult<TResult>
{
    public HPaginatedQueryResult()
    {
    }

    public HPaginatedQueryResult((List<TResult> Items, QueryPagination Pagination) result)
    {
        (Result, Pagination) = result;
    }

    public List<HateoasLink> Links { get; set; } = new();
}