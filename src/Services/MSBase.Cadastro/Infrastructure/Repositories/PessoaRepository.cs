using Microsoft.EntityFrameworkCore;
using MSBase.Cadastro.API.Entities;
using MSBase.Cadastro.API.Infrastructure.Context;
using MSBase.Core.Infrastructure;

namespace MSBase.Cadastro.API.Infrastructure.Repositories;

public class PessoaRepository : GenericRepository<Pessoa>, IPessoaRepository
{
    private readonly AppDbContext _context;

    public PessoaRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Pessoa> ObterPorId(Guid id)
    {
        return await Get().FirstOrDefaultAsync(x => x.Id == id);
    }
}