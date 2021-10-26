using System.Threading;
using System.Threading.Tasks;
using Cadastro.API.Requests;
using Cadastro.Application.Commands.EditarPessoaCommand;
using Cadastro.Application.Commands.NovaPessoaCommand;
using Cadastro.Application.Queries;
using CQRS.Core.API;
using CQRS.Core.API.Hateoas;
using CQRS.Core.Application.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Cadastro.API.Controllers
{
    [Route("/v1/pessoa")]
    public class PessoaController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IHateoasHelper _heateoasHelper;

        public PessoaController(
            IMediator mediator,
            IHateoasHelper heateoasHelper)
        {
            _mediator = mediator;
            _heateoasHelper = heateoasHelper;
        }

        [HttpGet(Name = nameof(Obter))]
        public async Task<IActionResult> Obter(ObterPessoasQueryInput input, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(input, cancellationToken);

            result.Links = _heateoasHelper.CreatePaginatedHLinks(
                nameof(Obter), 
                input, 
                result.Pagination);

            return HandleMediatorResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] NovaPessoaRequest request, CancellationToken cancellationToken)
        {
            var command = new NovaPessoaCommandInput(request.Nome, request.Idade);

            var result = await _mediator.Send(command, cancellationToken);
            
            return HandleMediatorResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] EditarPessoaRequest request, CancellationToken cancellationToken)
        {
            var command = new EditarPessoaCommandInput(request.PessoaId, request.NovaIdade);

            var result = await _mediator.Send(command, cancellationToken);
            
            return HandleMediatorResult(result);
        }
    }
}