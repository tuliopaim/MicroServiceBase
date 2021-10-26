using MediatR;

namespace CQRS.Core.Application.Mediator
{
    public interface IMediatorInput<out TMediatorResult> : IRequest<TMediatorResult> where TMediatorResult : IMediatorResult
    {

    }
}