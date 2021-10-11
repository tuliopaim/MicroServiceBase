namespace CQRS.Core.Bootstrap
{
    public interface IEnvironment
    {
        string this[string key] { get; }

        string Name { get; }
        
        bool IsDevelopment();
    }
}