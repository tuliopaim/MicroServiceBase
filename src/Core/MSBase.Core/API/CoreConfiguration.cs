using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace MSBase.Core.API;

public class CoreConfiguration
{
    public CoreConfiguration(IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }
    public bool ConfigureCqrs { get; private set; }
    public bool ConfigureHateoas { get; private set; } = true;
    public bool ConfigureRabbitMq { get; private set; } = true;
    public Assembly[] CqrsAssemblies { get; private set; }

    /// <summary>
    /// Enable Mediator/CQRS support, with input validation, logging and exception pipelines built-in
    /// </summary>
    public void WithCqrs(params Assembly[] cqrsAssemblies)
    {
        ArgumentNullException.ThrowIfNull(cqrsAssemblies);

        ConfigureCqrs = true;
        CqrsAssemblies = cqrsAssemblies;
    }
    
    /// <summary>
    /// Disable injection of RabbitMqConnection and RabbitMqProducer 
    /// </summary>
    public void WithoutRabbitMq()
    {
        ConfigureRabbitMq = false;
    }

    /// <summary>
    /// Enable injection of IHateoasHelper
    /// </summary>
    ///
    public void WithoutHateoasSupport()
    {
        ConfigureHateoas = false;
    }

}