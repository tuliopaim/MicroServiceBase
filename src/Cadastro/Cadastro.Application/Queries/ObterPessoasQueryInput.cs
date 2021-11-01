using Microsoft.AspNetCore.Mvc;
using MSBase.Core.Application.Queries;

namespace Cadastro.Application.Queries
{
    public class ObterPessoasQueryInput : PagedQueryInput<PagedQueryResult<ObterPessoasQueryResultItem>>
    {
        [FromQuery]
        public int Idade { get; set; }    
    }
}