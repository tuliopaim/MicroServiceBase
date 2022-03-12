using System.Text;
using MSBase.Core.Infrastructure.RabbitMq.Messages;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MSBase.Core.Extensions;

public static class RabbitExtensions
{
    public static RabbitMessage GetRabbitMessage(this BasicDeliverEventArgs rabbitEventArgs)
    {
        return JsonConvert.DeserializeObject<RabbitMessage>(Encoding.UTF8.GetString(rabbitEventArgs.Body.Span));
    }
        
    public static T GetDeserializedMessage<T>(this BasicDeliverEventArgs rabbitEventArgs)
    {
        var rabbitMessage = rabbitEventArgs.GetRabbitMessage();
        return JsonConvert.DeserializeObject<T>(rabbitMessage.SerializedMessage);
    }
    
    public static int? GetRetryCount(this BasicDeliverEventArgs rabbitEventArgs)
    {
        if (rabbitEventArgs.BasicProperties.Headers is null) return null;

        return rabbitEventArgs.BasicProperties.Headers.TryGetValue("x-retry-count", out var retryCountObj)
            ? (int)retryCountObj
            : null;
    }

    public static T GetDeserializedMessage<T>(this RabbitMessage rabbitMessage)
    {
        return JsonConvert.DeserializeObject<T>(rabbitMessage.SerializedMessage);
    }

    public static IBasicProperties SetRetryCountHeader(this IBasicProperties properties, int retryCount = 0)
    {
        properties.Headers = new Dictionary<string, object>
        {
            {"x-retry-count", retryCount}
        };

        return properties;
    }

}
