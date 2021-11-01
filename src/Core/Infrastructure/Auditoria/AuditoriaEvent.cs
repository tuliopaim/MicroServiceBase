using System.Collections.Generic;
using MSBase.Core.Infrastructure.Kafka;

namespace MSBase.Core.Infrastructure.Auditoria
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
