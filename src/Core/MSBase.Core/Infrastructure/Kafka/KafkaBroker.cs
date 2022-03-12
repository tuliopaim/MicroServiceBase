using System.Net;
using System.Text.Json;
using Confluent.Kafka;
using Confluent.Kafka.Admin;
using MSBase.Core.API;

namespace MSBase.Core.Infrastructure.Kafka
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

            CreateTopics(bootstrapServers);

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

        private void CreateTopics(string bootstrapServers)
        {
            using var adminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = bootstrapServers }).Build();

            foreach (var kafkaTopic in Enum.GetValues(typeof(KafkaTopics)).Cast<KafkaTopics>())
            {
                try
                {
                    adminClient.CreateTopicsAsync(new TopicSpecification[]
                    {
                        new TopicSpecification { Name = kafkaTopic.ToString(), ReplicationFactor = 1, NumPartitions = 1}
                    }).Wait();
                }
                catch (Exception)
                {
                }
            }
        }

        public async Task<Guid> PublishAsync<TMessage, TMessageType>(
            KafkaTopics topic,
            TMessageType messageType,
            TMessage message,
            CancellationToken cancellationToken) where TMessage : class, IKafkaMessage
        {
            var messageId = Guid.NewGuid();
            var plataformMessage = new PlatformMessage<TMessageType>(messageType, JsonSerializer.Serialize(message));

            await _producer.ProduceAsync(topic.ToString(), new Message<string, string>
            {
                Key = messageId.ToString(),
                Value = JsonSerializer.Serialize(plataformMessage)
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