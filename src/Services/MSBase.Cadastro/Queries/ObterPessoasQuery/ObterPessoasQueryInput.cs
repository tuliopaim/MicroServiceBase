using Microsoft.AspNetCore.Mvc;
using MSBase.Core.Application.Queries;

namespace MSBase.Cadastro.API.Queries.ObterPessoasQuery
{
    public class ObterPessoasQueryInput : PagedQueryInput<PagedQueryResult<ObterPessoasQueryResultItem>>
    {
        [FromQuery]
        public int Idade { get; set; }
    }
}