using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CQRS.Core.Application.Mediator
{
    public class LogPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IMediatorInput<TResponse>
        where TResponse : IMediatorResult
    {
        private readonly ILogger<LogPipelineBehavior<TRequest, TResponse>> _logger;

        public LogPipelineBehavior(ILogger<LogPipelineBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogDebug("{CommandType} - Entering handler!", typeof(TRequest).Name);

            var result = await next();

            _logger.LogDebug("{CommandType} - Leaving handler!", typeof(TRequest).Name);

            return result;
        }
    }
}