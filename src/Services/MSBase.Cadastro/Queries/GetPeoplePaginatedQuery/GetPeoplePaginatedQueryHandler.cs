using EasyCqrs.Queries;
using MSBase.Cadastro.API.Infrastructure.Repositories;
using MSBase.Core.Extensions;
using MSBase.Core.Queries;

namespace MSBase.Cadastro.API.Queries.GetPeoplePaginatedQuery;

public class GetPeoplePaginatedQueryHandler : IQueryHandler<GetPeopleQueryPaginatedInput, HPaginatedQueryResult<GetPeoplePaginatedResult>>
{
    private readonly IPersonRepository _personRepository;

    public GetPeoplePaginatedQueryHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<HPaginatedQueryResult<GetPeoplePaginatedResult>> Handle(GetPeopleQueryPaginatedInput query, CancellationToken cancellationToken)
    {
        var pessoasQuery = _personRepository.GetAsNoTracking();

        if (query.Idade != default)
        {
            pessoasQuery = pessoasQuery.Where(p => p.Idade == query.Idade);
        }

        var resultQuery = pessoasQuery.Select(p => new GetPeoplePaginatedResult
        {
            Id = p.Id,
            Nome = p.Nome,
            Idade = p.Idade,
            DataCriacao = p.DataCriacao,
        });

        var paginationTuple = await resultQuery.PaginateAsync(query.PageNumber, query.PageSize, cancellationToken);

        return new HPaginatedQueryResult<GetPeoplePaginatedResult>(paginationTuple);
    }
}