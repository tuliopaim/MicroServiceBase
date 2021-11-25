using EmailSender.Infrastructure;
using EmailSender.Templates.Views.Emails.PessoaCadastradaComSucesso;

namespace EmailSender.Domain
{
    public class EmailService : IEmailService
    {
        private readonly IEmailSender _emailSender;

        public EmailService(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task<bool> EnviarEmailPessoaCadastradaComSucesso(string nome, string email)
        {
            var model = new PessoaCadastradaComSucessoViewModel(nome, email, "Usuario cadastrado com sucesso!");

            return await _emailSender.SendEmail(model);
        }
    }
}
