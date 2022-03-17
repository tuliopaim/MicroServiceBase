using MediatR;

namespace MSBase.Core.Cqrs.Mediator;

public interface IMediatorInput<out TMediatorResult> 
    : IRequest<TMediatorResult> where TMediatorResult : IMediatorResult
{

}