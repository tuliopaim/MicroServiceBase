using CQRS.Core.Infrastructure;
using CQRS.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace CQRS.Domain.Repositories
{
    public interface IPessoaRepository : IGenericRepository<Pessoa>
    {
        Task<Pessoa> ObterPorId(Guid id);
    }
}