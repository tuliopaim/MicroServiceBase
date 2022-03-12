﻿using Core.Application.Commands;
using FluentValidation;

namespace Cadastro.API.Commands.EditarPessoaCommand
{
    public class EditarPessoaCommandValidator : CommandInputValidator<EditarPessoaCommandInput>
    {
        public EditarPessoaCommandValidator()
        {
            RuleFor(x => x.PessoaId).NotEmpty();

            RuleFor(x => x.NovaIdade).GreaterThanOrEqualTo((byte)18);
        }
    }
}
