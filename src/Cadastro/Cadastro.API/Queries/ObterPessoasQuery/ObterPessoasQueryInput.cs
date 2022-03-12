using Core.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Cadastro.API.Queries.ObterPessoasQuery
{
    public class ObterPessoasQueryInput : PagedQueryInput<PagedQueryResult<ObterPessoasQueryResultItem>>
    {
        [FromQuery]
        public int Idade { get; set; }
    }
}