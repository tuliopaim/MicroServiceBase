using Core.API;
using EmailSender.API.Domain;
using EmailSender.API.Requests;
using Microsoft.AspNetCore.Mvc;

namespace EmailSender.API.Controllers;

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
