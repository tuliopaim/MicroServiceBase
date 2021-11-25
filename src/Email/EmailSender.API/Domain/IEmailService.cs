namespace EmailSender.API.Domain
{
    public interface IEmailService
    {
        Task<bool> EnviarEmailPessoaCadastradaComSucesso(string nome, string email);
    }
}