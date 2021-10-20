using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Newtonsoft.Json;

namespace CQRS.Core.Application.Mediator
{
    public class FailFastPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IMediatorInput<TResponse>
        where TResponse : IMediatorResult
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public FailFastPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var falhas = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(e => e is not null)
                .ToList();

            return falhas.Any()
                ? Erros(falhas)
                : next();
        }

        private static Task<TResponse> Erros(IEnumerable<ValidationFailure> falhas)
        {
            var result = new MediatorResult();

            foreach (var falha in falhas)
            {
                result.AddError(falha.ErrorMessage);
            }

            var responseSerializado = JsonConvert.SerializeObject(result);
            var responseTipado = JsonConvert.DeserializeObject<TResponse>(responseSerializado);

            return Task.FromResult(responseTipado);
        }
    }
}