using Cadastro.Domain.Entities;
using MSBase.Core.Infrastructure;

namespace Cadastro.Domain.Repositories
{
    public interface IPessoaRepository : IGenericRepository<Pessoa>
    {
        Task<Pessoa> ObterPorId(Guid id);
    }
}