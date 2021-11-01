using MediatR;

namespace MSBase.Core.Application.Mediator
{
    public class Mediator : MediatR.Mediator, IMediator
    {
        public Mediator(ServiceFactory serviceFactory) : base(serviceFactory)
        {
        }
    }
}