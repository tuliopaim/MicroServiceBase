using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CQRS.Core.Application.Mediator
{
    public class ExceptionPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IMediatorInput<TResponse>
        where TResponse : IMediatorResult
    {
        private readonly ILogger<ExceptionPipelineBehavior<TRequest, TResponse>> _logger;

        public ExceptionPipelineBehavior(ILogger<ExceptionPipelineBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

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
            _logger.LogError(ex, "{CommandType} - Exception captured!", typeof(TRequest).Name);

            var result = new MediatorResult();

            result.AddError("Ocorreu um erro!");

            var resultSerializado = JsonConvert.SerializeObject(result);
            var resultTipado = JsonConvert.DeserializeObject<TResponse>(resultSerializado);

            return resultTipado;
        }
    }
}