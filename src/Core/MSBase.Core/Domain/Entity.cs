namespace MSBase.Core.Domain;

public abstract class Entity : IEntity
{
    protected Entity()
    {
        Id = Guid.NewGuid();
    }

    protected Entity(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }

    public override string ToString()
    {
        return Id.ToString();
    }
}