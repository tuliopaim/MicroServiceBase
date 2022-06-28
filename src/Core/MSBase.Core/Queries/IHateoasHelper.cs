using EasyCqrs.Queries;

namespace MSBase.Core.Queries;

public interface IHateoasHelper
{
    HateoasLink CreateGetByIdHateoasLink(string actionName, Guid id);
    HateoasLink CreateFirstHateoasLink(string actionName, object queryParams);
    HateoasLink CreateGetHateoasLink(string linkName, string actionName, object queryParams);
    HateoasLink CreateHateoasLink(HttpMethod httpMethod, string linkName, string actionName, object queryParams);
    HateoasLink CreateLastHateoasLink(string actionName, object queryParams);
    HateoasLink CreateNextHateoasLink(string actionName, object queryParams);
    HateoasLink CreatePrevHateoasLink(string actionName, object queryParams);
    HateoasLink CreateSelfHateoasLink(string actionName, object queryParams);
    List<HateoasLink> CreatePaginatedHateoasLinks<TQueryItem>(
        string actionName,
        PaginatedQueryInput<HPaginatedQueryResult<TQueryItem>> queryInput,
        QueryPagination paginationResult);
}