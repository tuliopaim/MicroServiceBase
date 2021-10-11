using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using CQRS.Core.Bootstrap;

namespace CQRS.Core.Infrastructure.Kafka
{
    public class KafkaBroker : IKafkaBroker
    {
        private readonly IProducer<string, string> _producer;
        private readonly ConsumerConfig _consumerConfig;

        public KafkaBroker(IEnvironment environment)
        {
            if (environment == null)
                throw new ArgumentNullException(nameof(environment));

            var bootstrapServers = environment["Kafka:BootstrapServers"];
            var groupId = environment["Kafka:GroupId"];

            _producer = new ProducerBuilder<string, string>(new ProducerConfig
            {
                BootstrapServers = bootstrapServers,
                ClientId = Dns.GetHostName()
            }).Build();

            _consumerConfig = new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                GroupId = groupId,
                AutoOffsetReset = AutoOffsetReset.Latest
            };
        }

        public async Task<Guid> PublishAsync<TEvent>(
            KafkaTopics topic,
            int eventType,
            TEvent @event,
            CancellationToken cancellationToken) where TEvent : class, IKafkaEvent
        {
            var messageId = Guid.NewGuid();
            var message = new PlatformMessage(eventType, JsonSerializer.Serialize(@event));

            await _producer.ProduceAsync(topic.ToString(), new Message<string, string>
            {
                Key = messageId.ToString(),
                Value = JsonSerializer.Serialize(new PlatformMessage(eventType, JsonSerializer.Serialize(@event)))
            }, cancellationToken);

            return messageId;
        }

        public IConsumer<string, string> GetConsumer(KafkaTopics topic)
        {
            return GetConsumer(new List<KafkaTopics> { topic });
        }

        public IConsumer<string, string> GetConsumer(IEnumerable<KafkaTopics> topics)
        {
            var consumer = new ConsumerBuilder<string, string>(_consumerConfig).Build();

            consumer.Subscribe(topics.Select(p => p.ToString()));

            return consumer;
        }
    }
}