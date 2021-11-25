namespace Core.API
{
    public class Environment : IEnvironment
    {
        private readonly Dictionary<string, string> _parameters;

        private const string DevelopmentEnvironmentName = "Development";

        public Environment(string name, Dictionary<string, string> parameters)
        {
            Name = name.Trim();
            _parameters = parameters;
        }

        public string Name { get; }

        public string this[string key] => _parameters.ContainsKey(key) ? _parameters[key] : string.Empty;

        public bool IsDevelopment()
        {
            return string.Equals(Name.Trim(), DevelopmentEnvironmentName, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}