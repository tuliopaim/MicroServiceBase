using Core.Infrastructure.Kafka;

namespace Core.Infrastructure.Auditoria
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
