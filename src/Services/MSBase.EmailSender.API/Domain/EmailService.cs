using MSBase.Core.API;
using MSBase.EmailSender.API.Infrastructure;
using MSBase.EmailSender.Templates.Views.Emails.PessoaCadastradaComSucesso;

namespace MSBase.EmailSender.API.Domain;

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