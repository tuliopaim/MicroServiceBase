using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MSBase.Core.Cqrs.Queries;

namespace MSBase.Core.Hateoas;

public class HateoasHelper : IHateoasHelper
{
    private readonly IHttpContextAccessor _accessor;
    private readonly LinkGenerator _generator;

    public HateoasHelper(IHttpContextAccessor accessor, LinkGenerator generator)
    {
        _accessor = accessor;
        _generator = generator;
    }

    public HateoasLink CreateHateoasLink(HttpMethod httpMethod, string linkName, string actionName, object queryParams)
    {
        ArgumentNullException.ThrowIfNull(_accessor.HttpContext);
        
        var link = _generator.GetUriByName(
            _accessor.HttpContext,
            actionName,
            queryParams);

        if (link is null)
        {
            throw new ArgumentException($"'{actionName}' may not correspond to a valid route name.");
        }

        return new HateoasLink
        {
            Name = FixCase(linkName),
            Method = httpMethod.ToString(),
            Href = link
        };
    }

    public HateoasLink CreateGetHateoasLink(string linkName, string actionName, object queryParams) =>
        CreateHateoasLink(HttpMethod.Get, linkName, actionName, queryParams);

    public HateoasLink CreateFirstHateoasLink(string actionName, object queryParams) =>
        CreateGetHateoasLink("_first", actionName, queryParams);

    public HateoasLink CreatePrevHateoasLink(string actionName, object queryParams) =>
        CreateGetHateoasLink("_prev", actionName, queryParams);

    public HateoasLink CreateSelfHateoasLink(string actionName, object queryParams) =>
        CreateGetHateoasLink("_self", actionName, queryParams);

    public HateoasLink CreateNextHateoasLink(string actionName, object queryParams) =>
        CreateGetHateoasLink("_next", actionName, queryParams);

    public HateoasLink CreateLastHateoasLink(string actionName, object queryParams) =>
        CreateGetHateoasLink("_last", actionName, queryParams);

    public IEnumerable<HateoasLink> CreatePaginatedHateoasLinks<TQueryItem>(
        string actionName,
        PagedQueryInput<PagedQueryResult<TQueryItem>> queryInput,
        QueryPaginationResult paginationResult)
    {
        var links = new List<HateoasLink>();

        AddFirstPaginatedLink(actionName, queryInput, paginationResult, links);
        AddPrevPaginatedLink(actionName, queryInput, paginationResult, links);
        AddSelfPaginatedLink(actionName, queryInput, paginationResult, links);
        AddNextPaginatedLink(actionName, queryInput, paginationResult, links);
        AddLastPaginatedLink(actionName, queryInput, paginationResult, links);

        return links;
    }

    private void AddFirstPaginatedLink<TQueryItem>(
        string actionName,
        PagedQueryInput<PagedQueryResult<TQueryItem>> queryInput,
        QueryPaginationResult paginationResult,
        ICollection<HateoasLink> links)
    {
        queryInput.PageNumber = paginationResult.FirstPage;

        links.Add(CreateFirstHateoasLink(actionName, queryInput));
    }

    private void AddPrevPaginatedLink<TQueryItem>(
        string actionName,
        PagedQueryInput<PagedQueryResult<TQueryItem>> queryInput,
        QueryPaginationResult paginationResult,
        ICollection<HateoasLink> links)
    {
        if (!paginationResult.HasPrevPage) return;

        queryInput.PageNumber = paginationResult.PrevPage;

        links.Add(CreatePrevHateoasLink(actionName, queryInput));
    }


    private void AddSelfPaginatedLink<TQueryItem>(
        string actionName,
        PagedQueryInput<PagedQueryResult<TQueryItem>> queryInput,
        QueryPaginationResult paginationResult,
        ICollection<HateoasLink> links)
    {
        queryInput.PageNumber = paginationResult.Number;

        links.Add(CreateSelfHateoasLink(actionName, queryInput));
    }

    private void AddNextPaginatedLink<TQueryItem>(
        string actionName,
        PagedQueryInput<PagedQueryResult<TQueryItem>> queryInput,
        QueryPaginationResult paginationResult,
        ICollection<HateoasLink> links)
    {
        if (!paginationResult.HasNextPage) return;

        queryInput.PageNumber = paginationResult.NextPage;

        links.Add(CreateNextHateoasLink(actionName, queryInput));
    }


    private void AddLastPaginatedLink<TQueryItem>(
        string actionName,
        PagedQueryInput<PagedQueryResult<TQueryItem>> queryInput,
        QueryPaginationResult paginationResult,
        ICollection<HateoasLink> links)
    {
        queryInput.PageNumber = paginationResult.LastPage;

        links.Add(CreateLastHateoasLink(actionName, queryInput));
    }

    private static string FixCase(string memberName)
    {
        return $"{memberName[..1].ToLower()}{memberName[1..]}";
    }

}