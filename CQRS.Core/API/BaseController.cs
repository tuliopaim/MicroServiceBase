using CQRS.Core.Application;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Core.API
{
    public class BaseController : ControllerBase
    {
        protected static IActionResult HandleMediatorResult(IMediatorResult mediatorResult, string location = null)
        {
            return mediatorResult is null ? new BadRequestResult() : mediatorResult.HandleMediatorResult();
        }
    }
}