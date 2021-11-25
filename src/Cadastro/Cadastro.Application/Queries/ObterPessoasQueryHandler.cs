using Cadastro.Domain.Repositories;
using Core.Application;
using Core.Application.Queries;

namespace Cadastro.Application.Queries
{
    public class ObterPessoasQueryHandler : IQueryHandler<ObterPessoasQueryInput, PagedQueryResult<ObterPessoasQueryResultItem>>
    {
        private readonly IPessoaRepository _pessoaRepository;

        public ObterPessoasQueryHandler(IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        public async Task<PagedQueryResult<ObterPessoasQueryResultItem>> Handle(ObterPessoasQueryInput query, CancellationToken cancellationToken)
        {
            var pessoasQuery = _pessoaRepository.GetAsNoTracking();

            if (query.Idade != default)
            {
                pessoasQuery = pessoasQuery.Where(p => p.Idade == query.Idade);
            }
            
            var resultQuery = pessoasQuery
                .Select(p => new ObterPessoasQueryResultItem
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Idade = p.Idade,
                    DataCriacao = p.DataCriacao,
                });

            return await resultQuery.PaginateAsync(query.PageNumber, query.PageSize, cancellationToken);
        }
    }
}