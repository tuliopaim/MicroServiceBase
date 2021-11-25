using Core.Infrastructure.Kafka;

namespace Core.Infrastructure.Email;

public abstract class EmailMessageBase : IKafkaMessage
{
    protected EmailMessageBase(string destinatario)
    {
        Destinatario = destinatario;
    }

    public string Destinatario { get; set; }
}

