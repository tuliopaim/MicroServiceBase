using MediatR;

namespace MSBase.Core.Cqrs.Mediator;

public class Mediator : MediatR.Mediator, IMediator
{
    public Mediator(ServiceFactory serviceFactory) : base(serviceFactory)
    {
    }
}