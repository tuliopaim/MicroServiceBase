using System.Collections.Generic;
using System.Linq;
using System.Net;
using CQRS.Core.Application;
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

        public static IActionResult HandleComReflection(this IMediatorResult mediatorResult)
        {
            if (!mediatorResult.IsValid())
            {
                return ReturnErrors(mediatorResult);
            }

            var resultDictionary = MontarDicionarioDeRetorno(mediatorResult);

            if (mediatorResult.HttpStatusCode != null)
            {
                return CustomResult(resultDictionary, mediatorResult.HttpStatusCode.Value);
            }
            
            return resultDictionary.Any() ? new OkObjectResult(resultDictionary) : new OkResult();
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
        
        private static Dictionary<string, object> MontarDicionarioDeRetorno(IMediatorResult mediatorResult)
        {
            var propriedadesIgnoradas = new List<string>
            {
                nameof(mediatorResult.HttpStatusCode),
                nameof(mediatorResult.Errors)
            };

            var resultDictionary = mediatorResult.GetType().GetProperties()
                .Where(property => !propriedadesIgnoradas.Contains(property.Name))
                .ToDictionary(property => property.Name, property => property.GetValue(mediatorResult));

            return resultDictionary;
        }

        private static IActionResult CustomResult(Dictionary<string, object> resultDictionary, HttpStatusCode statusCode)
        {
            return new ContentResult
            {
                StatusCode = (int)statusCode,
                ContentType = "application/json",
                Content = JsonConvert.SerializeObject(resultDictionary)
            };  
        }
    }
}