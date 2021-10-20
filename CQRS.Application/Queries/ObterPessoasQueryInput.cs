using CQRS.Core.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Application.Queries
{
    public class ObterPessoasQueryInput : PagedQueryInput<PagedQueryResult<ObterPessoasQueryResultItem>>
    {
        [FromQuery]
        public int Idade { get; set; }    
    }
}