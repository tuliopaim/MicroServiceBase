using MSBase.Core.Cqrs.Mediator;
using MSBase.Core.Hateoas;

namespace MSBase.Core.Cqrs.Queries;

public class QueryResult : MediatorResult
{
    public IEnumerable<HateoasLink> Links { get; set; }
}