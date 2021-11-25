using System.Text.Json;
using Confluent.Kafka;

namespace Core.Infrastructure.Kafka
{
    public static class KafkaExtensions
    {
        public static PlatformMessage<TMessageType> GetMessage<TMessageType>(
            this IConsumer<string, string> consumer,
            CancellationToken cancellationToken)
        {
            var consumerResult = consumer.Consume(cancellationToken);

            return JsonSerializer.Deserialize<PlatformMessage<TMessageType>>(consumerResult.Message.Value);
        }

        public static TMessage GetMessage<TMessage, TMessageType>(this PlatformMessage<TMessageType> platformMessage)
        {
            return JsonSerializer.Deserialize<TMessage>(platformMessage.Data);
        }
    }
}