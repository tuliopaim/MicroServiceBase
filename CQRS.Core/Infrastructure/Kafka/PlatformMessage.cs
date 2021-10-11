namespace CQRS.Core.Infrastructure.Kafka
{
    public class PlatformMessage
    {
        public PlatformMessage()
        {
        }

        public PlatformMessage(KafkaEventTypes eventType, string serializedEvent)
        {
            EventType = eventType;
            Data = serializedEvent;
        }

        public KafkaEventTypes EventType { get; set; }

        public string Data { get; set; }
    }
}