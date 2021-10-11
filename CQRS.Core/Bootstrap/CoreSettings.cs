using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace CQRS.Core.Bootstrap
{
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
        /// Nome do assembly do projeto de aplicação, utilizado para injetar as dependências
        /// </summary>
        public Type TipoDoApplicationMarker{ get; set; }
        
        /// <summary>
        /// Nome do Assembly do TipoDoApplicationMarker
        /// </summary>
        public string NomeDoAssemblyDoApplication() => TipoDoApplicationMarker.Assembly.GetName().Name;

        /// <summary>
        /// Tipo da classe do startup do serviço.
        /// </summary>
        public Type TipoDoStartup { get; set; }

        /// <summary>
        /// Configurar o Fail Fast Pipeline Behavior, para validar commands antes de chamar o Handler
        /// </summary>
        public bool ConfigurarFailFastPipelineBehavior { get; set; } = true;
        
        /// <summary>
        /// Interface IHostEnvironment, utilizada para a correta configuração da interface IEnvironment
        /// </summary>
        public IHostEnvironment HostEnvironment { get; set; }
        
        /// <summary>
        /// Configura a injeção da interface IEnvironment
        /// Depende da configuração correta da propriedade IHostEnvironment e IConfiguration
        /// </summary>
        public bool ConfigureIEnvironment { get; set; } = true;

        /// <summary>
        /// Configura e instancia o Broker do Kafka 
        /// </summary>
        public bool ConfigurarKafkaBroker { get; set; } = true;
    }
}