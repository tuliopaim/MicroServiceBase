using Core.Application.Queries;

namespace Cadastro.API.Queries.ObterPessoasQuery
{
    public class ObterPessoasQueryResultItem : IPagedQueryResultItem
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}