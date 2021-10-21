using CQRS.Core.Application.Mediator;
using CQRS.Core.Infrastructure.Kafka;
using System.Collections.Generic;

namespace CQRS.Core.Infrastructure.Auditoria
{
    public class AuditoriaEvent : IKafkaEvent
    {
        public AuditoriaEvent(IEnumerable<NovaAuditoriaDto> auditorias)
        {
            Auditorias = auditorias ?? new List<NovaAuditoriaDto>();
        }

        public IEnumerable<NovaAuditoriaDto> Auditorias { get; set; }
    }
}
