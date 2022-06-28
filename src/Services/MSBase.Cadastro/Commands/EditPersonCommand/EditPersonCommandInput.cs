using EasyCqrs.Commands;

namespace MSBase.Cadastro.API.Commands.EditPersonCommand;

public class EditPersonCommandInput : CommandInput<EditPersonCommandWithLinkResult>
{
    public EditPersonCommandInput(Guid pessoaId, byte novaIdade)
    {
        PessoaId = pessoaId;
        NovaIdade = novaIdade;
    }

    public Guid PessoaId { get; }
    public byte NovaIdade { get; }
}