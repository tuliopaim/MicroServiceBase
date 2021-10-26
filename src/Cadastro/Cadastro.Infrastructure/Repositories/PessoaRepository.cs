using System;
using System.Threading.Tasks;
using Cadastro.Domain.Entities;
using Cadastro.Domain.Repositories;
using Cadastro.Infrastructure.Context;
using CQRS.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;

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