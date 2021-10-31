using CQRS.Core.Application.Mediator;

namespace CQRS.Core.Application.Commands
{
    public class CommandResult : MediatorResult
    {
        public override CommandResult AddError(string error)
        {
            base.AddError(error);
            return this;
        }
    }
}