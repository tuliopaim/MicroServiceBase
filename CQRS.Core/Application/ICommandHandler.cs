using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace CQRS.Core.Application
{
    public interface ICommandHandler<in TCommandInput, TCommandResult> : IRequestHandler<TCommandInput, TCommandResult>
        where TCommandInput : IRequest<TCommandResult>, IMediatorInput<TCommandResult>
        where TCommandResult : IMediatorResult
    {
        new Task<TCommandResult> Handle(TCommandInput command, CancellationToken cancellationToken);
    }
}