using System;

namespace CQRS.Core.Domain
{
    public class Entity : IEntity
    {
        public Entity()
        {
            Id = Guid.NewGuid();
        }

        public Entity(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}