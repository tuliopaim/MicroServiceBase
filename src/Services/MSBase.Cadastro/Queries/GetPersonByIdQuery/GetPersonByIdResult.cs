namespace MSBase.Cadastro.API.Queries.GetPersonByIdQuery;

public class GetPersonByIdResult
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public int Idade { get; set; }
    public DateTime DataCriacao { get; set; }
}