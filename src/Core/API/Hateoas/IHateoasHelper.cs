using Core.Application.Queries;

namespace Core.API.Hateoas
{
    public interface IHateoasHelper
    {
        HLink CreateFirstHLink(string actionName, object queryParams);
        HLink CreateGetHLink(string relName, string actionName, object queryParams);
        HLink CreateHLink(HttpMethod httpMethod, string relName, string actionName, object queryParams);
        HLink CreateLastHLink(string actionName, object queryParams);
        HLink CreateNextHLink(string actionName, object queryParams);
        HLink CreatePrevHLink(string actionName, object queryParams);
        HLink CreateSelfHLink(string actionName, object queryParams);

        IEnumerable<HLink> CreatePaginatedHLinks<TQueryItem>(
            string actionName,
            PagedQueryInput<PagedQueryResult<TQueryItem>> queryInput,
            IQueryPaginationResult paginationResult)
            where TQueryItem : IPagedQueryResultItem;
    }
}