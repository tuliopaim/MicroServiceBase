using EasyCqrs.Mvc;
using EasyCqrs.Queries;
using Microsoft.AspNetCore.Mvc;
using MSBase.Core.Hateoas;

namespace MSBase.Core.API;

[ApiController]
public class BaseController : CqrsController
{
    public IActionResult HandlePaginatedResult<TQueryItem, TResult>(
        PaginatedQueryInput<HPaginatedQueryResult<TQueryItem>> queryInput,
        HPaginatedQueryResult<TResult> result,
        string actionLink,
        IHateoasHelper hateoasHelper)
    {
        result.Links = hateoasHelper.CreatePaginatedHateoasLinks(
            actionLink, queryInput, result.Pagination);

        return HandleResult(result);
    }

    public IActionResult HandleCreatedResult(
        CreatedCommandResult result,
        string getActionName,
        IHateoasHelper hateoasHelper)
    {
        result.Link = hateoasHelper.CreateGetByIdHateoasLink(getActionName, result.Id);

        return Ok(result);
    }

}