namespace MSBase.Core.Infrastructure.RabbitMq.Messages.Email;

public class EmailPessoaCadastradaComSucessoMessage : EmailMessageBase
{
    public EmailPessoaCadastradaComSucessoMessage(
        string destinatario,
        string nomePessoa) : base(destinatario)
    {
        NomePessoa = nomePessoa;
    }

    public string NomePessoa { get; private set; }
}

