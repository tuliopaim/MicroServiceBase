using System.Threading.Tasks;
using CQRS.API.Requests;
using CQRS.Application.Commands.NovaPessoaCommand;
using CQRS.Core.API;
using CQRS.Core.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.API.Controllers
{
    [Route("/v1/pessoa")]
    public class PessoaController : BaseController
    {
        private readonly IMediator _mediator;

        public PessoaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] NovaPessoaRequest request)
        {
            var command = new NovaPessoaCommandInput(request.Nome, request.Idade);

            var result = await _mediator.Send(command);

            return HandleMediatorResult(result, $"/v1/acordos/{result.Id}");
        }
    }
}