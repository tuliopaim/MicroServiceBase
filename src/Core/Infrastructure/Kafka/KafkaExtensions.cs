using System.Text.Json;
using Confluent.Kafka;

namespace Core.Infrastructure.Kafka
{
    public static class KafkaExtensions
    {
        public static PlatformMessage<TEventType> GetMessage<TEventType>(
            this IConsumer<string, string> consumer,
            CancellationToken cancellationToken)
        {
            var consumerResult = consumer.Consume(cancellationToken);

            return JsonSerializer.Deserialize<PlatformMessage<TEventType>>(consumerResult.Message.Value);
        }

        public static TEvent GetEvent<TEvent, TEventType>(this PlatformMessage<TEventType> platformMessage)
            => JsonSerializer.Deserialize<TEvent>(platformMessage.Data);
    }
}