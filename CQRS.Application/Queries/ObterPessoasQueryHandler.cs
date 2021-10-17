using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Core.Application;
using CQRS.Domain.Repositories;

namespace CQRS.Application.Queries
{
    public class ObterPessoasQueryHandler : IQueryHandler<ObterPessoasQueryInput, ObterPessoasQueryResult>
    {
        private readonly IPessoaRepository _pessoaRepository;

        public ObterPessoasQueryHandler(IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        public async Task<ObterPessoasQueryResult> Handle(ObterPessoasQueryInput query, CancellationToken cancellationToken)
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

            var paginatedResponse = await 
                resultQuery.PaginateAsync(query.PageNumber, query.PageSize, cancellationToken);

            return new ObterPessoasQueryResult
            {
                Result = paginatedResponse.Items,
                Pagination = new QueryPaginationResult
                {
                    Number = paginatedResponse.Number,
                    Size = paginatedResponse.Size,
                    TotalElements = paginatedResponse.TotalElements,
                }
            };
        }
    }
}