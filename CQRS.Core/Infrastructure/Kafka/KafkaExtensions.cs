using System.Text.Json;
using System.Threading;
using Confluent.Kafka;

namespace CQRS.Core.Infrastructure.Kafka
{
    public static class KafkaExtensions
    {
        public static PlatformMessage GetMessage(
            this IConsumer<string, string> consumer,
            CancellationToken cancellationToken)
        {
            var consumerResult = consumer.Consume(cancellationToken);

            return JsonSerializer.Deserialize<PlatformMessage>(consumerResult.Message.Value);
        }

        public static TEvent GetEvent<TEvent>(this PlatformMessage platformMessage)
            => JsonSerializer.Deserialize<TEvent>(platformMessage.Data);
    }
}