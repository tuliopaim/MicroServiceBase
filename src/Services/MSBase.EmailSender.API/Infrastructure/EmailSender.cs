using FluentEmail.Core;
using MSBase.EmailSender.Templates;
using MSBase.EmailSender.Templates.Views.Emails;

namespace MSBase.EmailSender.API.Infrastructure;

public class EmailSender : IEmailSender
{
    private readonly IFluentEmail _fluentEmail;
    private readonly ILogger<EmailSender> _logger;
    private readonly IRazorViewToStringRenderer _razorRenderer;

    public EmailSender(
        ILogger<EmailSender> logger,
        IFluentEmailFactory fluentEmailFactory,
        IRazorViewToStringRenderer razorRenderer)
    {
        _fluentEmail = fluentEmailFactory.Create();
        _logger = logger;
        _razorRenderer = razorRenderer;
    }

    public async Task<bool> SendEmail<T>(T model) where T : EmailBaseViewModel
    {
        const string viewPath = "/Views/Emails/PessoaCadastradaComSucesso/PessoaCadastradaComSucessoEmail.cshtml";

        try
        {
            var body = await _razorRenderer.RenderViewToStringAsync(viewPath, model);

            var sendResponse = await _fluentEmail
                .To(model.Email)
                .Subject(model.Assunto)
                .Body(body, true)
                .SendAsync();

            if (sendResponse.Successful) return true;

            _logger.LogError("Falha ao enviar email para {@Destinatarios}. Erros: {@Erros}",
                model.Email, sendResponse.ErrorMessages);

            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception capturada ao enviar email");
            return false;
        }
    }
}
