using Microsoft.EntityFrameworkCore;
using MSBase.Cadastro.API.Entities;
using MSBase.Cadastro.API.Infrastructure.Context;
using MSBase.Core.Infrastructure;

namespace MSBase.Cadastro.API.Infrastructure.Repositories;

public class PersonRepository : GenericRepository<Person>, IPersonRepository
{
    private readonly AppDbContext _context;

    public PersonRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Person> GetById(Guid id)
    {
        return await Get().FirstOrDefaultAsync(x => x.Id == id);
    }
}