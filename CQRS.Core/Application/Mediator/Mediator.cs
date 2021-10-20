using MediatR;
using IMediator = CQRS.Core.Application.Mediator.Mediator.IMediator;

namespace CQRS.Core.Application.Mediator
{
    public class Mediator : MediatR.Mediator, IMediator
    {
        public Mediator(ServiceFactory serviceFactory) : base(serviceFactory)
        {
        }
    }
}