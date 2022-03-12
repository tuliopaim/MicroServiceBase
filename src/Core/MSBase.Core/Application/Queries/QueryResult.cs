using MSBase.Core.API.Hateoas;
using MSBase.Core.Application.Mediator;

namespace MSBase.Core.Application.Queries
{
    public class QueryResult : MediatorResult
    {
        public IEnumerable<HLink> Links { get; set; }
    }
}