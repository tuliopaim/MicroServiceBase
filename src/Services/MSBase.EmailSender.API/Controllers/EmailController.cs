using Microsoft.AspNetCore.Mvc;
using MSBase.Core.API;
using MSBase.EmailSender.API.Domain;
using MSBase.EmailSender.API.Requests;

namespace MSBase.EmailSender.API.Controllers;

public class EmailController : BaseController
{
    private readonly IEmailService _emailService;

    public EmailController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpPost("cadastrado-com-sucesso")]
    public async Task<IActionResult> Novo(PessoaCadastradaComSucessoRequest request)
    {
        var result = await _emailService.EnviarEmailPessoaCadastradaComSucesso(request.Nome, request.Email);

        return result ? Ok() : BadRequest();
    }
}
