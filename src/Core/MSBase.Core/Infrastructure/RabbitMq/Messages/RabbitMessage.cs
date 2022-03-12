namespace MSBase.Core.Infrastructure.RabbitMq.Messages;

public class RabbitMessage
{
    public RabbitMessage(
        string serializedMessage, 
        Type messageType)
    {
        SerializedMessage = serializedMessage;
        MessageType = messageType;
    }

    public string SerializedMessage { get; private set; }
    public Type MessageType { get; private set; }
}