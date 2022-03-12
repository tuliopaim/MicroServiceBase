namespace MSBase.Core.API
{
    public interface IEnvironment
    {
        string this[string key] { get; }
        string Name { get; }
        bool IsDevelopment { get; }
    }
}