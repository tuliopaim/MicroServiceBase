using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MSBase.Core.Application.Queries;

namespace MSBase.Core.API.Hateoas
{
    public class HateoasHelper : IHateoasHelper
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly LinkGenerator _generator;

        public HateoasHelper(IHttpContextAccessor accessor, LinkGenerator generator)
        {
            _accessor = accessor;
            _generator = generator;
        }

        public HLink CreateHLink(HttpMethod httpMethod, string relName, string actionName, object queryParams)
        {
            var link = _generator.GetUriByName(
                _accessor.HttpContext,
                actionName,
                queryParams);

            if (link is null)
            {
                throw new ArgumentException(
                    $"'{actionName}' may not correspond to a valid route name. " +
                    "Check use of 'nameOf' on route names and helper parameters, as indicated " +
                    "in documents and uriParameters keys.");
            }

            return new HLink
            {
                Rel = FixCase(relName),
                Method = httpMethod.ToString(),
                Href = link
            };
        }

        public HLink CreateGetHLink(string relName, string actionName, object queryParams) =>
            CreateHLink(HttpMethod.Get, relName, actionName, queryParams);

        public HLink CreateFirstHLink(string actionName, object queryParams) =>
             CreateGetHLink("_first", actionName, queryParams);

        public HLink CreatePrevHLink(string actionName, object queryParams) =>
            CreateGetHLink("_prev", actionName, queryParams);

        public HLink CreateSelfHLink(string actionName, object queryParams) =>
            CreateGetHLink("_self", actionName, queryParams);

        public HLink CreateNextHLink(string actionName, object queryParams) =>
            CreateGetHLink("_next", actionName, queryParams);

        public HLink CreateLastHLink(string actionName, object queryParams) =>
            CreateGetHLink("_last", actionName, queryParams);

        public IEnumerable<HLink> CreatePaginatedHLinks<TQueryItem>(
            string actionName,
            PagedQueryInput<PagedQueryResult<TQueryItem>> queryInput,
            IQueryPaginationResult paginationResult)
            where TQueryItem : IPagedQueryResultItem
        {
            var links = new List<HLink>();

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
            IQueryPaginationResult paginationResult,
            ICollection<HLink> links) where TQueryItem : IPagedQueryResultItem
        {
            queryInput.PageNumber = paginationResult.FirstPage;

            links.Add(CreateFirstHLink(actionName, queryInput));
        }

        private void AddPrevPaginatedLink<TQueryItem>(
            string actionName,
            PagedQueryInput<PagedQueryResult<TQueryItem>> queryInput,
            IQueryPaginationResult paginationResult,
            ICollection<HLink> links)
            where TQueryItem : IPagedQueryResultItem
        {
            if (!paginationResult.HasPrevPage) return;

            queryInput.PageNumber = paginationResult.PrevPage;

            links.Add(CreatePrevHLink(actionName, queryInput));
        }


        private void AddSelfPaginatedLink<TQueryItem>(
            string actionName,
            PagedQueryInput<PagedQueryResult<TQueryItem>> queryInput,
            IQueryPaginationResult paginationResult,
            ICollection<HLink> links)
            where TQueryItem : IPagedQueryResultItem
        {
            queryInput.PageNumber = paginationResult.Number;

            links.Add(CreateSelfHLink(actionName, queryInput));
        }

        private void AddNextPaginatedLink<TQueryItem>(
            string actionName,
            PagedQueryInput<PagedQueryResult<TQueryItem>> queryInput,
            IQueryPaginationResult paginationResult,
            ICollection<HLink> links)
            where TQueryItem : IPagedQueryResultItem
        {
            if (!paginationResult.HasNextPage) return;

            queryInput.PageNumber = paginationResult.NextPage;

            links.Add(CreateNextHLink(actionName, queryInput));
        }


        private void AddLastPaginatedLink<TQueryItem>(
            string actionName,
            PagedQueryInput<PagedQueryResult<TQueryItem>> queryInput,
            IQueryPaginationResult paginationResult,
            ICollection<HLink> links)
            where TQueryItem : IPagedQueryResultItem
        {
            queryInput.PageNumber = paginationResult.LastPage;

            links.Add(CreateLastHLink(actionName, queryInput));
        }

        private static string FixCase(string memberName)
        {
            return $"{memberName[..1].ToLower()}{memberName[1..]}";
        }

    }
}