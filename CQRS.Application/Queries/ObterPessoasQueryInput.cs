using CQRS.Core.Application;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Application.Queries
{
    public class ObterPessoasQueryInput : PagedQueryInput<ObterPessoasQueryResult>
    {
        [FromQuery]
        public int Idade { get; set; }    
    }
}