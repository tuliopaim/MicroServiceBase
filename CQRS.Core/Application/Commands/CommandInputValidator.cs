using CQRS.Core.Application.Mediator;

namespace CQRS.Core.Application.Commands
{
    public class CommandInputValidator<TCommandInput> : MediatorInputValidator<TCommandInput>
    {
    }
}