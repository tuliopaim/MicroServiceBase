using Confluent.Kafka;

namespace MSBase.Core.Infrastructure.Kafka
{
    public interface IKafkaBroker
    {
        IConsumer<string, string> GetConsumer(IEnumerable<KafkaTopics> topics);
        IConsumer<string, string> GetConsumer(KafkaTopics topic);
        Task<Guid> PublishAsync<TEvent, TEventType>(
            KafkaTopics topic, 
            TEventType eventType,
            TEvent @event, 
            CancellationToken cancellationToken) 
            where TEvent : class, IKafkaEvent;
    }
}