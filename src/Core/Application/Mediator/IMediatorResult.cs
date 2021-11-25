namespace Core.Application.Mediator
{
    public interface IMediatorResult
    {
        IEnumerable<string> Errors { get; }

        IMediatorResult AddError(string error);

        bool IsValid();
    }
}