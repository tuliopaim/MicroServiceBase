using Microsoft.Extensions.Configuration;

namespace MSBase.Core.API;

public class CoreSettings
{
    /// <summary>
    /// Interface IConfiguration
    /// </summary>
    public IConfiguration Configuration { get; set; }

    /// <summary>
    /// Configurar ou não o Mediator
    /// </summary>
    public bool ConfigurarMediator { get; set; } = true;

    /// <summary>
    /// Tipo da classe do startup do serviço.
    /// </summary>
    public Type TipoDoStartup { get; set; }

    /// <summary>
    /// Nome do assembly do projeto de aplicação, utilizado para injetar as dependências
    /// </summary>
    public Type TipoDaCamadaDeApplication { get; set; }
        
    /// <summary>
    /// Configura e instancia RabbitMqConnection e RabbitMqProducer 
    /// </summary>
    public bool ConfigurarRabbitMq { get; set; } = true;    
        
    /// <summary>
    /// Injetar IHateoasHelper
    /// </summary>
    public bool ConfigurarHateoasHelper { get; set; } = true;
}