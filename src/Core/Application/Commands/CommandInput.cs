using Core.Application.Mediator;

namespace Core.Application.Commands
{
    public class CommandInput<TCommandResult> : MediatorInput<TCommandResult> where TCommandResult : CommandResult
    {
    }
}