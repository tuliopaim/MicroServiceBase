using System;

namespace CQRS.Core.Domain
{
    public interface IEntity
    {
        Guid Id { get; }
    }
}