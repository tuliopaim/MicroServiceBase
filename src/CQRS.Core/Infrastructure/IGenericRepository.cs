using System.Linq;
using MSBase.Core.Domain;

namespace MSBase.Core.Infrastructure
{
    public interface IGenericRepository<TEntity> where TEntity : Entity
    {
        IUnitOfWork UnitOfWork { get; }
        void Add(TEntity entity);
        IQueryable<TEntity> Get();
        IQueryable<TEntity> GetAsNoTracking();
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}