using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MSBase.Core.API
{
    [ExcludeFromCodeCoverage]
    public class RunConsumersService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly List<Type> _consumersHandlersTypes;

        public RunConsumersService(
            IServiceProvider serviceProvider, 
            CoreSettings settings)
        {
            _serviceProvider = serviceProvider;

            _consumersHandlersTypes = settings.TipoDoStartup.Assembly
                .ExportedTypes
                .Select(t => t.GetTypeInfo())
                .Where(t => t.IsClass && !t.IsAbstract && t.IsAssignableTo(typeof(IConsumerHandler)))
                .Select(t => t.AsType())
                .ToList();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if(!_consumersHandlersTypes.Any()) return;

            using var scope = _serviceProvider.CreateScope();
            
            var eventConsumersTasks = new Dictionary<IConsumerHandler, Task>();

            RunAllConsumers(stoppingToken, eventConsumersTasks, scope);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.WhenAny(eventConsumersTasks.Values);

                foreach (var faultedTask in eventConsumersTasks.Values.Where(task => task.IsFaulted))
                {
                    RunFaultedConsumer(stoppingToken, eventConsumersTasks, faultedTask);
                }
            }
        }
        private void RunAllConsumers(CancellationToken stoppingToken,
            Dictionary<IConsumerHandler, Task> eventConsumersTasks, 
            IServiceScope serviceScope)
        {
            foreach (var consumerType in _consumersHandlersTypes)
            {
                var consumerHandler = (IConsumerHandler)serviceScope.ServiceProvider.GetService(consumerType);

                eventConsumersTasks.Add(consumerHandler!, 
                    Task.Run(async () => await consumerHandler.Handle(stoppingToken), stoppingToken));
            }
        }

        private static void RunFaultedConsumer(
            CancellationToken stoppingToken, 
            Dictionary<IConsumerHandler, Task> eventConsumersTasks,
            Task faultedTask)
        {
            var faultedTaskService = eventConsumersTasks
                .First(x => x.Value == faultedTask).Key;

            eventConsumersTasks[faultedTaskService] =
                Task.Run(async () => await faultedTaskService.Handle(stoppingToken), stoppingToken);
        }
    }
}