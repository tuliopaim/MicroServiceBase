using MSBase.Core.Application.Commands;

namespace Cadastro.Application.Commands.NovaPessoaCommand
{
    public class NovaPessoaCommandInput : CommandInput<NovaPessoaCommandResult>
    {
        public NovaPessoaCommandInput(string nome, byte idade)
        {
            Nome = nome;
            Idade = idade;
        }

        public string Nome { get; private set; }
        public byte Idade { get; private set; }
    }
}