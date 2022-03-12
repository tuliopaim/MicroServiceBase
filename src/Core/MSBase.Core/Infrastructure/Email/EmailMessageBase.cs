using MSBase.Core.Infrastructure.Kafka;

namespace MSBase.Core.Infrastructure.Email;

public abstract class EmailMessageBase : IKafkaMessage
{
    protected EmailMessageBase(string destinatario)
    {
        Destinatario = destinatario;
    }

    public string Destinatario { get; set; }
}

