using Core.Application.Mediator;

namespace Core.Application.Commands
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