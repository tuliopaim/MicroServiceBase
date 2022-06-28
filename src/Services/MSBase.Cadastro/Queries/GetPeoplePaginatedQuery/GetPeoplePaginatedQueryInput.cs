using EasyCqrs.Queries;
using Microsoft.AspNetCore.Mvc;
using MSBase.Core.Queries;

namespace MSBase.Cadastro.API.Queries.GetPeoplePaginatedQuery;

public class GetPeopleQueryPaginatedInput : QueryPaginatedInput<HPaginatedQueryResult<GetPeoplePaginatedResult>>
{
    [FromQuery]
    public int Idade { get; set; }
}