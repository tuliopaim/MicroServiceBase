using EasyCqrs.Queries;
using MSBase.Cadastro.API.Infrastructure.Repositories;

namespace MSBase.Cadastro.API.Queries.GetPersonByIdQuery;

public class GetPersonByIdQueryHandler : IQueryHandler<GetPersonByIdQueryInput, QueryResult<GetPersonByIdResult>>
{
    private readonly IPersonRepository _personRepository;

    public GetPersonByIdQueryHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<QueryResult<GetPersonByIdResult>> Handle(GetPersonByIdQueryInput request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.GetById(request.Id);

        var personResult = person is null
            ? null
            : new GetPersonByIdResult
            {
                Id = person.Id,
                Nome = person.Nome,
                Idade = person.Idade,
                DataCriacao = person.DataCriacao,
            };

        return new QueryResult<GetPersonByIdResult>()
        {
            Result = personResult
        };
    }
}