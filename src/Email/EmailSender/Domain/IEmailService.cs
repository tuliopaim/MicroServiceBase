
namespace EmailSender.Domain
{
    public interface IEmailService
    {
        Task<bool> EnviarEmailPessoaCadastradaComSucesso(string nome, string email);
    }
}