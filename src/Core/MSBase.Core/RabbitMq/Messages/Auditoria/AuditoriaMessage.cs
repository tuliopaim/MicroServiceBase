namespace MSBase.Core.RabbitMq.Messages.Auditoria;

public class AuditoriaMessage
{
    public AuditoriaMessage(IEnumerable<NovaAuditoriaDto> auditorias)
    {
        Auditorias = auditorias ?? new List<NovaAuditoriaDto>();
    }

    public IEnumerable<NovaAuditoriaDto> Auditorias { get; set; }
}