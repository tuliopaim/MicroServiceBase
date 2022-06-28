using MediatR;
using Microsoft.AspNetCore.Mvc;
using MSBase.Cadastro.API.Commands.EditPersonCommand;
using MSBase.Cadastro.API.Commands.NewPersonCommand;
using MSBase.Cadastro.API.Queries.GetPeoplePaginatedQuery;
using MSBase.Cadastro.API.Queries.GetPersonByIdQuery;
using MSBase.Cadastro.API.Requests;
using MSBase.Core.API;
using MSBase.Core.Queries;

namespace MSBase.Cadastro.API.Controllers;

[ApiController]
[Route("/v1/person")]
public class PersonController : BaseController
{
    private readonly IMediator _mediator;

    public PersonController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}", Name = nameof(GetById))]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetPersonByIdQueryInput(id));

        return HandleResult(result);
    }

    [HttpGet(Name = nameof(GetPaginatedPeople))]
    [Produces(typeof(HPaginatedQueryResult<GetPeoplePaginatedResult>))]
    public async Task<IActionResult> GetPaginatedPeople(
        [FromServices] IHateoasHelper heateoasHelper,
        [FromQuery] GetPeopleQueryPaginatedInput input, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(input, cancellationToken);

        return HandlePaginatedResult(
            input,
            result,
            nameof(GetPaginatedPeople),
            heateoasHelper);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] NovaPessoaRequest request,
        CancellationToken cancellationToken)
    {
        var command = new NewPersonCommandInput(request.Nome, request.Email, request.Idade);

        var result = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { result.Id }, result);
    }

    [HttpPut]
    public async Task<IActionResult> Edit([FromBody] EditarPessoaRequest request, CancellationToken cancellationToken)
    {
        var command = new EditPersonCommandInput(request.PessoaId, request.NovaIdade);

        var result = await _mediator.Send(command, cancellationToken);

        return HandleResult(result);
    }
}