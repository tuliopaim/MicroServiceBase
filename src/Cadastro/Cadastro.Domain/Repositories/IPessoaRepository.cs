using System;
using System.Threading.Tasks;
using Cadastro.Domain.Entities;
using CQRS.Core.Infrastructure;

namespace Cadastro.Domain.Repositories
{
    public interface IPessoaRepository : IGenericRepository<Pessoa>
    {
        Task<Pessoa> ObterPorId(Guid id);
    }
}