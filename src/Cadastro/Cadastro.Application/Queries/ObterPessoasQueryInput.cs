using CQRS.Core.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Cadastro.Application.Queries
{
    public class ObterPessoasQueryInput : PagedQueryInput<PagedQueryResult<ObterPessoasQueryResultItem>>
    {
        [FromQuery]
        public int Idade { get; set; }    
    }
}