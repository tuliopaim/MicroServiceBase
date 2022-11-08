using EasyCqrs.Commands;
using MSBase.Core.Commands;
using MSBase.Core.Queries;

namespace MSBase.Cadastro.API.Commands.NewPersonCommand;

public class NewPersonCommandInput : CommandInput<CreatedCommandResult>
{
    public NewPersonCommandInput(string nome, string email, byte idade)
    {
        Nome = nome;
        Email = email;
        Idade = idade;
    }

    public string Nome { get; private set; }
    public string Email { get; set; }
    public byte Idade { get; private set; }
}