using MSBase.EmailSender.API.Infrastructure;
using MSBase.EmailSender.Templates.Views.Emails.PessoaCadastradaComSucesso;

namespace MSBase.EmailSender.API.Domain;

public class EmailService : IEmailService
{
    private readonly IEmailSender _emailSender;
    private readonly IConfiguration _configuration;

    public EmailService(IEmailSender emailSender, IConfiguration configuration)
    {
        _emailSender = emailSender;
        _configuration = configuration;
    }

    public async Task<bool> EnviarEmailPessoaCadastradaComSucesso(string nome, string email)
    {
        var assunto = _configuration["EmailAssuntos:PessoaCadastradaComSucesso"];

        var model = new PessoaCadastradaComSucessoViewModel(nome, email, assunto);

        return await _emailSender.SendEmail(model);
    }
}