using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Core.Application;
using CQRS.Domain.Repositories;

namespace CQRS.Application.Queries
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
                    Nome = p.Nome,
                    Idade = p.Idade,
                });

            return await resultQuery.PaginateAsync(query.PageNumber, query.PageSize, cancellationToken);
        }
    }
}