using EasyCqrs.Commands;
using EasyCqrs.Queries;

namespace MSBase.Core.Queries;

public class CommandWithLinkResult : CommandResult
{
    public HateoasLink Link { get; set; } = new();
}

public class QueryWithLinkResult<TResult> : QueryResult<TResult>
{
    public List<HateoasLink> Links { get; set; } = new();
}

public class HQueryListResult<TResult> : QueryListResult<TResult>
{
    public List<HateoasLink> Links { get; set; } = new();
}