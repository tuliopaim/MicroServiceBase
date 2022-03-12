using MSBase.Cadastro.API.Infrastructure.Repositories;
using MSBase.Core.Application;
using MSBase.Core.Application.Queries;

namespace MSBase.Cadastro.API.Queries.ObterPessoasQuery;

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

        var resultQuery = pessoasQuery.Select(p => new ObterPessoasQueryResultItem
        {
            Id = p.Id,
            Nome = p.Nome,
            Idade = p.Idade,
            DataCriacao = p.DataCriacao,
        });

        return await resultQuery.PaginateAsync(query.PageNumber, query.PageSize, cancellationToken);
    }
}