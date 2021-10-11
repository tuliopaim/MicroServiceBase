using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Core.Application.Kafka
{
    public interface IKafkaBroker
    {
        IConsumer<string, string> GetConsumer(IEnumerable<KafkaTopics> topics);
        IConsumer<string, string> GetConsumer(KafkaTopics topic);
        Task<Guid> PublishAsync<TEvent>(KafkaTopics topic, int eventType, TEvent @event, CancellationToken cancellationToken) where TEvent : class, IKafkaEvent;
    }
}