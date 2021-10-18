using System.Collections.Generic;
using CQRS.Core.API.Hateoas;

namespace CQRS.Core.Application
{
    public class QueryResult : MediatorResult
    {
        public IEnumerable<HLink> Links { get; set; }
    }
}