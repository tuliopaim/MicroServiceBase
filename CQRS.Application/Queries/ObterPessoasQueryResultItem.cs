﻿using CQRS.Core.Application.Queries;

namespace CQRS.Application.Queries
{
    public class ObterPessoasQueryResultItem : IPagedQueryResultItem
    {
        public string Nome { get; set; }
        public int Idade { get; set; }
    }
}