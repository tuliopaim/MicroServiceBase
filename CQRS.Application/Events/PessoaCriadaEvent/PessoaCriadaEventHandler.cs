using System.Threading;
using System.Threading.Tasks;
using CQRS.Core.Application;
using Microsoft.Extensions.Logging;

namespace CQRS.Application.Events.PessoaCriadaEvent
{
    public class PessoaCriadaEventHandler : IEventHandler<PessoaCriadaEventInput>
    {
        private readonly ILogger _logger;

        public PessoaCriadaEventHandler(ILogger logger)
        {
            _logger = logger;
        }

        public Task Handle(PessoaCriadaEventInput @event, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Evento {nameof(PessoaCriadaEventHandler)} disparado!");

            return Task.CompletedTask;
        }
    }
}