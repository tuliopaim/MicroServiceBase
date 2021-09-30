namespace CQRS.Core.Application
{
    public class CommandInput<TCommandResult> : MediatorInput<TCommandResult> where TCommandResult : CommandResult
    {
    }
}