using MediatR;

namespace CQRS.Core.Application.Mediator
{
    public class Mediator : MediatR.Mediator, IMediator
    {
        public Mediator(ServiceFactory serviceFactory) : base(serviceFactory)
        {
        }
    }
}