using System;

namespace CQRS.Core.Domain
{
    public class AuditableEntity : Entity, IAuditableEntity
    {
        public DateTime DataCriacao { get; }
        public DateTime? DataAlteracao { get; }
    }
}           