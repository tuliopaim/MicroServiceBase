namespace Core.Infrastructure.Kafka
{
    public class PlatformMessage<TEventType>
    {
        public PlatformMessage()
        {
        }

        public PlatformMessage(TEventType eventType, string serializedEvent)
        {
            EventType = eventType;
            Data = serializedEvent;
        }

        public TEventType EventType { get; set; }

        public string Data { get; set; }
    }
}