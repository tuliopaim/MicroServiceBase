namespace MSBase.Core.Cqrs.Mediator;

public interface IMediatorResult
{
    IEnumerable<string> Errors { get; }

    IMediatorResult AddError(string error);

    bool IsValid();
}