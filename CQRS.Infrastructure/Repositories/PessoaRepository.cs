using CQRS.Core.Infrastructure;
using CQRS.Domain.Entities;
using CQRS.Domain.Repositories;
using CQRS.Infrastructure.Context;

namespace CQRS.Infrastructure.Repositories
{
    public class PessoaRepository : GenericRepository<Pessoa>, IPessoaRepository
    {
        private readonly AppDbContext _context;

        public PessoaRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}