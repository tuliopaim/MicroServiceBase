using MSBase.Core.Cqrs.Mediator;

namespace MSBase.Core.Cqrs.Commands;

public class CommandInputValidator<TCommandInput> : MediatorInputValidator<TCommandInput>
{
}