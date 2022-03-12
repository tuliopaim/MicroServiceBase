using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MSBase.Core.Extensions;
using MSBase.Core.Infrastructure.RabbitMq.Messages;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MSBase.Core.Infrastructure.RabbitMq;

public abstract class RabbitMqConsumerBackgroundService : BackgroundService
{
    private readonly ILogger<RabbitMqConsumerBackgroundService> _logger;
    private readonly string _queueName;

    protected RabbitMqConsumerBackgroundService(
        RabbitMqConnection rabbitConnection,
        ILogger<RabbitMqConsumerBackgroundService> logger,
        string queueName)
    {
        ArgumentNullException.ThrowIfNull(queueName, nameof(queueName));

        _logger = logger;
        _queueName = queueName;

        var connection = rabbitConnection.Connection;

        Channel = connection.CreateModel();
    }

    protected uint PrefetchSize { get; set; } = 0;
    protected ushort PrefetchCount { get; set; } = 1;
    protected int MaxRetryCount { get; set; } = 5;

    protected IModel Channel { get; set; }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogDebug("{BackgroundService} started", nameof(RabbitMqConsumerBackgroundService));

        DeclareQueues();

        StartConsumer();

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }

        _logger.LogDebug("{BackgroundService} finished", nameof(RabbitMqConsumerBackgroundService));
    }

    private void DeclareQueues()
    {
        foreach (var queue in RoutingKeys.All)
        {
            _ = Channel.QueueDeclare(queue, true, false, false);
        }
    }

    private void StartConsumer()
    {
        if (!RoutingKeys.All.Contains(_queueName))
        {
            throw new ArgumentException($"Invalid queue name, please register in [{typeof(RoutingKeys).AssemblyQualifiedName}]");
        }

        PrefetchCount = PrefetchCount < 1 ? (ushort)1 : PrefetchCount;

        Channel.BasicQos(PrefetchSize, PrefetchCount, false);

        var consumer = new AsyncEventingBasicConsumer(Channel);

        consumer.Received += MessageReceived;

        Channel.BasicConsume(_queueName, false, consumer);
    }

    private async Task MessageReceived(object sender, BasicDeliverEventArgs rabbitEventArgs)
    {
        try
        {
            var message = rabbitEventArgs.GetRabbitMessage();

            _logger.LogInformation("{MessageType} received on {ConsumerType}", message.MessageType.ToString(), GetType().Name);

            var result = await HandleMessage(rabbitEventArgs, message);

            if (!result)
            {
                OnError(rabbitEventArgs);
                return;
            }

            Channel.BasicAck(rabbitEventArgs.DeliveryTag, false);
        }
        catch (Exception ex)
        {
            OnException(rabbitEventArgs, ex);
        }
    }

    protected abstract Task<bool> HandleMessage(BasicDeliverEventArgs rabbitEventArgs, RabbitMessage message);

    /// <summary>
    /// Nacks if x-retry-count >= MaxRetryCount, requeue if not
    /// </summary>
    /// <param name="rabbitEventArgs"></param>
    protected virtual void OnError(BasicDeliverEventArgs rabbitEventArgs)
    {
        RepublicarSeNecessario(rabbitEventArgs);

        Channel.BasicNack(rabbitEventArgs.DeliveryTag, false, false);
    }

    /// <summary>
    /// Logs the Exception, nacks if x-retry-count >= MaxRetryCount, requeue if not
    /// </summary>
    /// <param name="rabbitEventArgs"></param>
    /// <param name="ex"></param>
    protected virtual void OnException(BasicDeliverEventArgs rabbitEventArgs, Exception ex)
    {
        _logger.LogError(ex, "Exception captured on {ConsumerType}", GetType().Name);

        RepublicarSeNecessario(rabbitEventArgs);

        Channel.BasicNack(rabbitEventArgs.DeliveryTag, false, false);
    }

    private void RepublicarSeNecessario(BasicDeliverEventArgs rabbitEventArgs)
    {
        var retryCount = rabbitEventArgs.GetRetryCount();

        if (retryCount >= MaxRetryCount) return;

        var propriedades = Channel.CreateBasicProperties();
        propriedades.Persistent = true;
        propriedades.SetRetryCountHeader();

        Channel.BasicPublish("",
            rabbitEventArgs.RoutingKey,
            propriedades,
            rabbitEventArgs.Body);
    }

    public override void Dispose()
    {
        Channel?.Close(200, "Goodbye");
        Channel?.Dispose();

        base.Dispose();
    }
}
