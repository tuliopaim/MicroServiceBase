using MediatR;

namespace CQRS.Core.Application
{
    public interface IMediatorInput<out TMediatorResult> : IRequest<TMediatorResult> where TMediatorResult : IMediatorResult
    {

    }
}