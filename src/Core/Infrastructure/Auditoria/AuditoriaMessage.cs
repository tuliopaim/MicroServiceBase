using Core.Infrastructure.Kafka;

namespace Core.Infrastructure.Auditoria
{
    public class AuditoriaMessage : IKafkaMessage
    {
        public AuditoriaMessage(IEnumerable<NovaAuditoriaDto> auditorias)
        {
            Auditorias = auditorias ?? new List<NovaAuditoriaDto>();
        }

        public IEnumerable<NovaAuditoriaDto> Auditorias { get; set; }
    }
}
