namespace Core.Domain
{
    public abstract class AuditableEntity : Entity, IAuditableEntity
    {
        protected AuditableEntity()
        {
        }

        protected AuditableEntity(Guid id) : base(id)
        {
        }

        public DateTime DataCriacao { get; }
        public DateTime? DataAlteracao { get; }
    }
}