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

    public CoreConfiguration(
        IConfiguration configuration,
        params Assembly[] cqrsAssemblies) : this(configuration)
    {
        ArgumentNullException.ThrowIfNull(cqrsAssemblies);

        ConfigureCqrs = true;
        ConfigureHateoasHelper = true;
        ConfigureRabbitMq = true;
        CqrsAssemblies = cqrsAssemblies;
    }
    
    public IConfiguration Configuration { get; }
    public bool ConfigureCqrs { get; private set; }
    public bool ConfigureHateoasHelper { get; private set; }
    public bool ConfigureRabbitMq { get; private set; }
    public Assembly[] CqrsAssemblies { get; private set; }

    /// <summary>
    /// Enable Mediator/CQRS suport
    /// </summary>
    public void EnableCqrsSupport(params Assembly[] cqrsAssemblies)
    {
        ArgumentNullException.ThrowIfNull(cqrsAssemblies);

        ConfigureCqrs = true;
        CqrsAssemblies = cqrsAssemblies;
    }

    /// <summary>
    /// Enable injection of RabbitMqConnection and RabbitMqProducer 
    /// </summary>
    public void EnableRabbitMqSupport()
    {
        ConfigureRabbitMq = true;
    }

    /// <summary>
    /// Enable injection of IHateoasHelper
    /// </summary>
    ///
    public void EnableHateoasSupport()
    {
        ConfigureHateoasHelper = true;
    }
}