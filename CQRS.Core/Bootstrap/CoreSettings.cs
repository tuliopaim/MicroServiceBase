using System;
using Microsoft.Extensions.Configuration;

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
        /// Nome do assembly do projeto de aplicação
        /// </summary>
        public string NomeDoApplicationAssembly { get; set; }
        
        /// <summary>
        /// Tipo da classe do startup do serviço.
        /// </summary>
        public Type TipoDoStartup { get; set; }

    }
}