using CQRS.Core.Application.Queries;
using System;

namespace CQRS.Application.Queries
{
    public class ObterPessoasQueryResultItem : IPagedQueryResultItem
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public DateTime DataCriacao{ get; set; }
    }
}