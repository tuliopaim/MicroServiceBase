using CQRS.Core.Application.Mediator;
using System.Collections.Generic;

namespace CQRS.Core.Infrastructure
{
    public class AuditoriaEvent : EventInput
    {
        public AuditoriaEvent(IEnumerable<NovaAuditoriaDto> auditorias)
        {
            Auditorias = auditorias ?? new List<NovaAuditoriaDto>();
        }

        public IEnumerable<NovaAuditoriaDto> Auditorias { get; set; }
    }
}
