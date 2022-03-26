using EasyCqrs.Commands;
using EasyCqrs.Queries;

namespace MSBase.Core.Hateoas;

public class HCommandResult : CommandResult
{
    public HateoasLink Link { get; set; } = new();
}

public class CreatedCommandResult : HCommandResult
{
    public Guid Id { get; set; }
}

public class HQueryResult<TResult> : QueryResult<TResult>
{
    public List<HateoasLink> Links { get; set; } = new();
}

public class HListQueryResult<TResult> : ListQueryResult<TResult>
{
    public List<HateoasLink> Links { get; set; } = new();
}

public class HPaginatedQueryResult<TResult> : PaginatedQueryResult<TResult>
{
    public HPaginatedQueryResult()
    {
    }

    public HPaginatedQueryResult((List<TResult> Items, QueryPagination Pagination) result)
    {
        Result = result.Items;
        Pagination = result.Pagination;
    }

    public List<HateoasLink> Links { get; set; } = new();
}