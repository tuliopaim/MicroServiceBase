using CQRS.Core.Application.Mediator;

namespace CQRS.Core.Application.Commands
{
    public class CommandInput<TCommandResult> : MediatorInput<TCommandResult> where TCommandResult : CommandResult
    {
    }
}