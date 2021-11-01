using System.Linq;
using Microsoft.EntityFrameworkCore;
using MSBase.Core.Domain;

namespace MSBase.Core.Infrastructure
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : Entity
    {
        private readonly EfDbContext _context;

        protected GenericRepository(EfDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;
                
        public IQueryable<TEntity> Get() => _context.Set<TEntity>().AsQueryable();

        public IQueryable<TEntity> GetAsNoTracking() => Get().AsNoTrackingWithIdentityResolution();

        public void Add(TEntity entity) => _context.Add(entity);

        public void Update(TEntity entity) => _context.Entry(entity).State = EntityState.Modified;

        public void Remove(TEntity entity) => _context.Remove(entity);
    }
}