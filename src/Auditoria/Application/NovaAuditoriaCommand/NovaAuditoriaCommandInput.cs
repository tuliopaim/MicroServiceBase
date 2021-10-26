using System.Collections.Generic;
using CQRS.Core.Application.Commands;

namespace AuditoriaAPI.Application.NovaAuditoriaCommand
{
    public class NovaAuditoriaCommandInput : CommandInput<NovaAuditoriaCommandResult>
    {
        public IEnumerable<NovaAuditoriaCommandInputItem> Auditorias { get; init; }
    }
}