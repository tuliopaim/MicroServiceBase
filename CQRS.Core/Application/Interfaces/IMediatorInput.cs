using MediatR;

namespace CQRS.Core.Application.Interfaces
{
    public interface IMediatorInput<out TMediatorResult> : IRequest<TMediatorResult> where TMediatorResult : IMediatorResult
    {

    }
}