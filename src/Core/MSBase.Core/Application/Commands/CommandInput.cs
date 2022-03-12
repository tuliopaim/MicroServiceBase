using MSBase.Core.Application.Mediator;

namespace MSBase.Core.Application.Commands;

public class CommandInput<TCommandResult> : MediatorInput<TCommandResult> where TCommandResult : CommandResult
{
}