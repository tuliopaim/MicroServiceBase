using Confluent.Kafka;

namespace Core.Infrastructure.Kafka
{
    public interface IKafkaBroker
    {
        IConsumer<string, string> GetConsumer(IEnumerable<KafkaTopics> topics);
        IConsumer<string, string> GetConsumer(KafkaTopics topic);
        Task<Guid> PublishAsync<TMessage, TMessageType>(
            KafkaTopics topic,
            TMessageType messageType,
            TMessage message,
            CancellationToken cancellationToken)
            where TMessage : class, IKafkaMessage;
    }
}