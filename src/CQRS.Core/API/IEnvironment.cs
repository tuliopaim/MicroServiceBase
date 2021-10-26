namespace CQRS.Core.API
{
    public interface IEnvironment
    {
        string this[string key] { get; }

        string Name { get; }

        bool IsDevelopment();
    }
}