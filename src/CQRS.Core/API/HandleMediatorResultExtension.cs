using System.Net;
using CQRS.Core.Application.Mediator;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CQRS.Core.API
{
    public static class HandleMediatorResultExtension
    {
        public static IActionResult HandleMediatorResult(this IMediatorResult mediatorResult)
        {
            if (!mediatorResult.IsValid())
            {
                return ReturnErrors(mediatorResult);
            }

            if (mediatorResult.HttpStatusCode != null)
            {
                return CustomResult(mediatorResult, mediatorResult.HttpStatusCode.Value);
            }

            return new OkObjectResult(mediatorResult);
        }
        
        private static IActionResult ReturnErrors(IMediatorResult mediatorResult)
        {
            return new ContentResult
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                ContentType = "application/json",
                Content = JsonConvert.SerializeObject(new
                {
                    mediatorResult.Errors
                })
            };
        }
        
        private static IActionResult CustomResult(IMediatorResult result, HttpStatusCode statusCode)
        {
            return new ContentResult
            {
                StatusCode = (int)statusCode,
                ContentType = "application/json",
                Content = JsonConvert.SerializeObject(result)
            };  
        }
    }
}