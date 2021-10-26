using CQRS.Core.Application.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Core.API
{
    public class BaseController : ControllerBase
    {
        protected static IActionResult HandleMediatorResult(IMediatorResult mediatorResult)
        {
            return mediatorResult is null ? new BadRequestResult() : mediatorResult.HandleMediatorResult();
        }
    }
}