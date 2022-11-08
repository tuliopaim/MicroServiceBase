using EasyCqrs.Mediator;
using EasyCqrs.Mvc;
using EasyCqrs.Queries;
using Microsoft.AspNetCore.Mvc;
using MSBase.Core.Commands;
using MSBase.Core.Queries;

namespace MSBase.Core.API;

public class BaseController : CqrsController
{
    protected IActionResult HandlePaginatedResult<TQueryItem, TResult>(
        QueryPaginatedInput<HPaginatedQueryResult<TQueryItem>> queryInput,
        HPaginatedQueryResult<TResult> result,
        string actionLink,
        IHateoasHelper hateoasHelper)
    {
        result.Links = hateoasHelper.CreatePaginatedHateoasLinks(
            actionLink, queryInput, result.Pagination);

        return HandleResult(result);
    }

    protected IActionResult HandleCreateResult<TCreatedResult>(TCreatedResult result, string actionName)
        where TCreatedResult : CreatedCommandResult
    {
        if (result.Exception != null)
        {
            return StatusCode(500);
        }

        if (!result.IsValid)
        {
            return BadRequest(new { result.IsValid, result.Errors });
        }

        return CreatedAtAction(actionName, new { result.Id }, result);
    }
}