using MediatR;
using IMediator = CQRS.Core.Application.IMediator;

namespace CQRS.Core.Application
{
    public class Mediator : MediatR.Mediator, IMediator
    {
        public Mediator(ServiceFactory serviceFactory) : base(serviceFactory)
        {
        }
    }
}