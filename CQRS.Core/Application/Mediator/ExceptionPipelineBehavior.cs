using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Newtonsoft.Json;

namespace CQRS.Core.Application.Mediator
{
    public class ExceptionPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IMediatorInput<TResponse>
        where TResponse : IMediatorResult
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();                 
            }
            catch (Exception ex)
            {
                return ExceptionTratada(ex);        
            }
        }

        private TResponse ExceptionTratada(Exception ex)
        {
            Console.WriteLine(ex.ToString());

            var result = new MediatorResult();

            result.AddError("Ocorreu um erro!");

            var resultSerializado = JsonConvert.SerializeObject(result);
            var resultTipado = JsonConvert.DeserializeObject<TResponse>(resultSerializado);

            return resultTipado;
        }
    }
}