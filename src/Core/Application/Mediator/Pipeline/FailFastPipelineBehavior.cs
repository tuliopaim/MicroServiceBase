using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MSBase.Core.Application.Mediator.Pipeline
{
    public class FailFastPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IMediatorInput<TResponse>
        where TResponse : IMediatorResult
    {
        private readonly ILogger<FailFastPipelineBehavior<TRequest, TResponse>> _logger;
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public FailFastPipelineBehavior(
            ILogger<FailFastPipelineBehavior<TRequest, TResponse>> logger,
            IEnumerable<IValidator<TRequest>> validators)
        {
            _logger = logger;
            _validators = validators;
        }

        public Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var failures = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(e => e is not null)
                .ToList();

            _logger.LogDebug("{RequestType} - Validated with {ValidationErrorQuantity} error(s).",
                typeof(TRequest).Name,
                failures.Count);

            return failures.Any()
                ? ReturnError(failures)
                : next();
        }

        private Task<TResponse> ReturnError(IEnumerable<ValidationFailure> failures)
        {
            var result = new MediatorResult();

            foreach (var fail in failures)
            {
                _logger.LogError("{RequestType} - Validation error: {ValidationError}",
                    typeof(TRequest).Name,
                    fail.ErrorMessage);

                result.AddError(fail.ErrorMessage);
            }

            var serializedResult = JsonConvert.SerializeObject(result);
            var typedResult = JsonConvert.DeserializeObject<TResponse>(serializedResult);

            return Task.FromResult(typedResult);
        }
    }
}