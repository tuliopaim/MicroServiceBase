using EasyCqrs.Queries;
using Microsoft.AspNetCore.Mvc;
using MSBase.Core.Hateoas;

namespace MSBase.Cadastro.API.Queries.GetPeoplePaginatedQuery;

public class GetPeoplePaginatedQueryInput : PaginatedQueryInput<HPaginatedQueryResult<GetPeoplePaginatedResult>>
{
    [FromQuery]
    public int Idade { get; set; }
}