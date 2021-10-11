using CQRS.Core.Application;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Core.API
{
    public class BaseController : ControllerBase
    {
        protected static IActionResult HandleMediatorResult(IMediatorResult mediatorResult, string location = null)
        { 
            if (mediatorResult is null) return new BadRequestResult();
            
            return mediatorResult.HandleSemReflection();
        }
    }
}