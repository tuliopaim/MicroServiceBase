using Cadastro.Domain.Entities;
using Cadastro.Domain.Repositories;
using Cadastro.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using MSBase.Core.Infrastructure;

namespace Cadastro.Infrastructure.Repositories
{
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
}