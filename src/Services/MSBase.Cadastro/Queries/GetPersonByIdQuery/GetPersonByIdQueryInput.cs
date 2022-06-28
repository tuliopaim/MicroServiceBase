using EasyCqrs.Queries;

namespace MSBase.Cadastro.API.Queries.GetPersonByIdQuery;

public class GetPersonByIdQueryInput : QueryInput<QueryResult<GetPersonByIdResult>>
{
    public GetPersonByIdQueryInput(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}