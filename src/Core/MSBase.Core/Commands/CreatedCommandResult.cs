using EasyCqrs.Commands;

namespace MSBase.Core.Commands;

public class CreatedCommandResult : CommandResult
{
    public Guid Id { get; set; }

    public static implicit operator CreatedCommandResult(Guid id) => new() { Id = id };
}