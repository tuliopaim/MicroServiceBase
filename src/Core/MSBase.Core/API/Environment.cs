using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace MSBase.Core.API
{
    public class Environment : IEnvironment
    {
        private readonly IConfiguration _configuration;

        private const string DevelopmentEnvironmentName = "Development";

        public Environment(IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            _configuration = configuration;
            Name = hostEnvironment.EnvironmentName;
        }

        public string Name { get; }

        public string this[string key] => _configuration[key];

        public bool IsDevelopment =>
            string.Equals(Name.Trim(), DevelopmentEnvironmentName, StringComparison.CurrentCultureIgnoreCase);
    }
}