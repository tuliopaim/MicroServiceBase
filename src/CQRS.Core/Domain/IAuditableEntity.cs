using System;

namespace CQRS.Core.Domain
{
    public interface IAuditableEntity
    {
        Guid Id { get; }
        DateTime DataCriacao { get; }
        DateTime? DataAlteracao { get; }
    }
}