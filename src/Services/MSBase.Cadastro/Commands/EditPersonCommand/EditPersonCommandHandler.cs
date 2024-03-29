﻿using EasyCqrs.Commands;
using MSBase.Cadastro.API.Infrastructure.Repositories;

namespace MSBase.Cadastro.API.Commands.EditPersonCommand;

public class EditPersonCommandHandler : ICommandHandler<EditPersonCommandInput, EditPersonCommandResult>
{
    private readonly IPersonRepository _personRepository;

    public EditPersonCommandHandler(
        IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<EditPersonCommandResult> Handle(EditPersonCommandInput command, CancellationToken cancellationToken)
    {
        var pessoa = await _personRepository.GetById(command.PessoaId);

        if (pessoa is null)
        {
            var result = new EditPersonCommandResult();
            result.AddError("Person não encontrada");

            return null;
        }

        pessoa.AlterarIdade(command.NovaIdade);

        await _personRepository.UnitOfWork.CommitAsync(cancellationToken);

        return new EditPersonCommandResult();
    }
}