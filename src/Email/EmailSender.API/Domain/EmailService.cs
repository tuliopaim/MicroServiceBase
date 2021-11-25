using Core.API;
using EmailSender.API.Infrastructure;
using EmailSender.Templates.Views.Emails.PessoaCadastradaComSucesso;

namespace EmailSender.API.Domain
{
    public class EmailService : IEmailService
    {
        private readonly IEmailSender _emailSender;
        private readonly IEnvironment _environment;

        public EmailService(IEmailSender emailSender, IEnvironment environment)
        {
            _emailSender = emailSender;
            _environment = environment;
        }

        public async Task<bool> EnviarEmailPessoaCadastradaComSucesso(string nome, string email)
        {
            var assunto = _environment["EmailAssuntos:PessoaCadastradaComSucesso"];

            var model = new PessoaCadastradaComSucessoViewModel(nome, email, assunto);

            return await _emailSender.SendEmail(model);
        }
    }
}
