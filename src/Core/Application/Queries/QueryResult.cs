using Core.API.Hateoas;
using Core.Application.Mediator;

namespace Core.Application.Queries
{
    public class QueryResult : MediatorResult
    {
        public IEnumerable<HLink> Links { get; set; }
    }
}