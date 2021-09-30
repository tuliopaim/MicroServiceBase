using CQRS.Core.Application;

namespace CQRS.Application.Commands.NovaPessoaCommand
{
    public class NovaPessoaCommandInput : CommandInput<NovaPessoaCommandResult>
    {
        public NovaPessoaCommandInput(string nome, ushort idade)
        {
            Nome = nome;
            Idade = idade;
        }

        public string Nome { get; private set; }
        public ushort Idade { get; private set; }
    }
}