namespace MSBase.Cadastro.API.Queries.GetPeoplePaginatedQuery;

public class GetPeoplePaginatedResult
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public int Idade { get; set; }
    public DateTime DataCriacao { get; set; }
}