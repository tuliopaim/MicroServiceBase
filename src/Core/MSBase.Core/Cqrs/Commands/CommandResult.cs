using MSBase.Core.Cqrs.Mediator;

namespace MSBase.Core.Cqrs.Commands;

public class CommandResult : MediatorResult
{
    public override CommandResult AddError(string error)
    {
        base.AddError(error);
        return this;
    }
}