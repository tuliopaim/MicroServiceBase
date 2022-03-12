using System.Text;
using System.Text.RegularExpressions;
using MSBase.Core.Infrastructure.RabbitMq.Messages;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MSBase.Core.Extensions;

public static class RabbitExtensions
{
    public static RabbitMessage GetRabbitMessage(this BasicDeliverEventArgs rabbitEventArgs)
    {
        var body = rabbitEventArgs.Body.ToArray();
        return JsonConvert.DeserializeObject<RabbitMessage>(Encoding.UTF8.GetString(body));
    }

    public static T GetDeserializedMessage<T>(this BasicDeliverEventArgs rabbitEventArgs)
    {
        var rabbitMessage = rabbitEventArgs.GetRabbitMessage();
        return JsonConvert.DeserializeObject<T>(rabbitMessage.SerializedMessage);
    }

    public static int? GetRetryCount(this BasicDeliverEventArgs rabbitEventArgs)
    {
        return rabbitEventArgs.BasicProperties.Headers.TryGetValue("x-retry-count", out var retryCountObj)
            ? (int)retryCountObj
            : null;
    }

    public static T GetDeserializedMessage<T>(this RabbitMessage rabbitMessage)
    {
        return JsonConvert.DeserializeObject<T>(rabbitMessage.SerializedMessage);
    }

    public static IBasicProperties SetRetryCountHeader(this IBasicProperties properties)
    {
        properties.Headers = new Dictionary<string, object>
        {
            {"x-retry-count", 0}
        };

        return properties;
    }

}
