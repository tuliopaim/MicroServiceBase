using Cadastro.API.Entities;
using Core.Infrastructure;

namespace Cadastro.API.Infrastructure.Repositories
{
    public interface IPessoaRepository : IGenericRepository<Pessoa>
    {
        Task<Pessoa> ObterPorId(Guid id);
    }
}