namespace MSBase.Core.Infrastructure.RabbitMq.Messages.Email;

public abstract class EmailMessageBase
{
    protected EmailMessageBase(string destinatario)
    {
        Destinatario = destinatario;
    }

    public string Destinatario { get; set; }
}

