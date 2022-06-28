using EasyCqrs.Mvc;
using EasyCqrs.Queries;
using Microsoft.AspNetCore.Mvc;
using MSBase.Core.Queries;

namespace MSBase.Core.API;

public class BaseController : CqrsController
{
    protected IActionResult HandlePaginatedResult<TQueryItem, TResult>(
        PaginatedQueryInput<HPaginatedQueryResult<TQueryItem>> queryInput,
        HPaginatedQueryResult<TResult> result,
        string actionLink,
        IHateoasHelper hateoasHelper)
    {
        result.Links = hateoasHelper.CreatePaginatedHateoasLinks(
            actionLink, queryInput, result.Pagination);

        return HandleResult(result);
    }
}