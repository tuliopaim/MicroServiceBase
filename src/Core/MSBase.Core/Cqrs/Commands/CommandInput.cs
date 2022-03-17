using MSBase.Core.Cqrs.Mediator;

namespace MSBase.Core.Cqrs.Commands;

public class CommandInput<TCommandResult> : MediatorInput<TCommandResult> where TCommandResult : CommandResult
{
}