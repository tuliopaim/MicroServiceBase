using CQRS.Core.Domain;
using System;
using System.Linq;

namespace CQRS.Core.Infrastructure
{
    public interface IGenericRepository<TEntity> where TEntity : Entity
    {
        IUnitOfWork UnitOfWork { get; }

        void Add(TEntity entity);
        IQueryable<TEntity> Get();
        IQueryable<TEntity> GetAsNoTracking();
        void Remove(Guid id);
        void Update(TEntity entity);
    }
}