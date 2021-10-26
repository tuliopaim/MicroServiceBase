using CQRS.Core.Infrastructure;
using CQRS.Domain.Entities;
using CQRS.Domain.Repositories;
using CQRS.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CQRS.Infrastructure.Repositories
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