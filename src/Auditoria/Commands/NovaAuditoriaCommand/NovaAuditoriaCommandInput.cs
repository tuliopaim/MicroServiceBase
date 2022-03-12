using Core.Application.Commands;

namespace Auditoria.API.Commands.NovaAuditoriaCommand
{
    public class NovaAuditoriaCommandInput : CommandInput<NovaAuditoriaCommandResult>
    {
        public IEnumerable<NovaAuditoriaCommandInputItem> Auditorias { get; init; }
    }
}