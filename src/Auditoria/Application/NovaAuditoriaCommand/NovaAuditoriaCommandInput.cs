using System.Collections.Generic;
using MSBase.Core.Application.Commands;

namespace Auditoria.API.Application.NovaAuditoriaCommand
{
    public class NovaAuditoriaCommandInput : CommandInput<NovaAuditoriaCommandResult>
    {
        public IEnumerable<NovaAuditoriaCommandInputItem> Auditorias { get; init; }
    }
}