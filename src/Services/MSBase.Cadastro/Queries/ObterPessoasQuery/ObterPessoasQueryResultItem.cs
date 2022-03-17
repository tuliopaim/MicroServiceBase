using MSBase.Core.Cqrs.Queries;

namespace MSBase.Cadastro.API.Queries.ObterPessoasQuery;

public class ObterPessoasQueryResultItem
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public int Idade { get; set; }
    public DateTime DataCriacao { get; set; }
}