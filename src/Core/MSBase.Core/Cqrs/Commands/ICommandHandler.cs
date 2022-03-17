using MediatR;
using MSBase.Core.Cqrs.Mediator;

namespace MSBase.Core.Cqrs.Commands;

public interface ICommandHandler<in TCommandInput, TCommandResult> : IMediatorHandler<TCommandInput, TCommandResult>
    where TCommandInput : IRequest<TCommandResult>, IMediatorInput<TCommandResult>
    where TCommandResult : IMediatorResult
{
    new Task<TCommandResult> Handle(TCommandInput command, CancellationToken cancellationToken);
}