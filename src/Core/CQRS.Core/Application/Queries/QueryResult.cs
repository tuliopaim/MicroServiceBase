using System.Collections.Generic;
using CQRS.Core.API.Hateoas;
using CQRS.Core.Application.Mediator;

namespace CQRS.Core.Application.Queries
{
    public class QueryResult : MediatorResult
    {
        public IEnumerable<HLink> Links { get; set; }
    }
}