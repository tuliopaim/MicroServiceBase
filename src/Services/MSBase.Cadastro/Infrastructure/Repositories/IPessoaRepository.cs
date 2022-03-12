using MSBase.Cadastro.API.Entities;
using MSBase.Core.Infrastructure;

namespace MSBase.Cadastro.API.Infrastructure.Repositories
{
    public interface IPessoaRepository : IGenericRepository<Pessoa>
    {
        Task<Pessoa> ObterPorId(Guid id);
    }
}