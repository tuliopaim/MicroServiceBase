using MSBase.Cadastro.API.Entities;
using MSBase.Core.Infrastructure;

namespace MSBase.Cadastro.API.Infrastructure.Repositories;

public interface IPersonRepository : IGenericRepository<Person>
{
    Task<Person> GetById(Guid id);
}