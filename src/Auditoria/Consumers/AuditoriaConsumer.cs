using System;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Auditoria.API.Application.NovaAuditoriaCommand;
using Confluent.Kafka;
using CQRS.Core.API;
using CQRS.Core.Application.Mediator;
using CQRS.Core.Infrastructure.Auditoria;
using CQRS.Core.Infrastructure.Kafka;
using CQRS.Core.Infrastructure.Kafka.KafkaEventTypes;
using Microsoft.Extensions.DependencyInjection;

namespace Auditoria.API.Consumers
{
    public class AuditoriaConsumer : IConsumerHandler
    {
        private readonly IConsumer<string, string> _consumer;
        private readonly IServiceProvider _serviceProvider;

        public AuditoriaConsumer(IKafkaBroker kafkaBroker, IServiceProvider serviceProvider)
        {
            _consumer = kafkaBroker.GetConsumer(KafkaTopics.AuditoriaTopic);
            this._serviceProvider = serviceProvider;
        }

        public async Task Handle(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var message = _consumer.GetMessage<AuditoriaEventTypes>(cancellationToken);

                using var scope = _serviceProvider.CreateScope();

                var mediator = scope.ServiceProvider.GetService<IMediator>();

                var @event = JsonSerializer.Deserialize<AuditoriaEvent>(message.Data);

                var command = MapearParaCommand(@event);
                
                var result = await mediator!.Send(command, cancellationToken);
            }
        }

        private static NovaAuditoriaCommandInput MapearParaCommand(AuditoriaEvent @event)
        {
            return new NovaAuditoriaCommandInput
            {
                Auditorias = @event.Auditorias.Select(a =>
                    new NovaAuditoriaCommandInputItem(
                        a.TipoAuditoria,
                        a.IdEntidade,
                        a.NomeEntidade,
                        a.NomeTabela,
                        a.Propriedades.Select(p => new NovaAuditoriaPropriedadeCommandInputItem(
                            p.NomeDaPropriedade,
                            p.NomeDaColuna,
                            p.ValorAntigo,
                            p.ValorNovo,
                            p.EhChavePrimaria)).ToList()))
            };
        }
    }
}
