using MSBase.Core.Cqrs.Queries;

namespace MSBase.Core.Hateoas;

public interface IHateoasHelper
{
    HateoasLink CreateFirstHateoasLink(string actionName, object queryParams);
    HateoasLink CreateGetHateoasLink(string linkName, string actionName, object queryParams);
    HateoasLink CreateHateoasLink(HttpMethod httpMethod, string linkName, string actionName, object queryParams);
    HateoasLink CreateLastHateoasLink(string actionName, object queryParams);
    HateoasLink CreateNextHateoasLink(string actionName, object queryParams);
    HateoasLink CreatePrevHateoasLink(string actionName, object queryParams);
    HateoasLink CreateSelfHateoasLink(string actionName, object queryParams);

    IEnumerable<HateoasLink> CreatePaginatedHateoasLinks<TQueryItem>(
        string actionName,
        PagedQueryInput<PagedQueryResult<TQueryItem>> queryInput,
        QueryPaginationResult paginationResult);
}