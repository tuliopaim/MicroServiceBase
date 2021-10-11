namespace CQRS.Core.Infrastructure.Kafka
{
    public class PlatformMessage
    {
        public PlatformMessage(int eventType, string serializedEvent)
        {
            EventType = eventType;
            Data = serializedEvent;
        }

        public int EventType { get; }

        public string Data { get; }
    }
}