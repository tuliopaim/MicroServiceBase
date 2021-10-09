namespace CQRS.Core.CrossCutting
{
    public interface IEnvironment
    {
        string this[string key] { get; }

        string Name { get; }
        
        bool IsDevelopment();
    }
}