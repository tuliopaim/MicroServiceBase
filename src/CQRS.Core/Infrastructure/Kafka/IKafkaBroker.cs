using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace CQRS.Core.Infrastructure.Kafka
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