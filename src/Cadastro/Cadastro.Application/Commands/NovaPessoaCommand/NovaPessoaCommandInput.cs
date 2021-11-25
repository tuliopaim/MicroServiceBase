using Core.Application.Commands;

namespace Cadastro.Application.Commands.NovaPessoaCommand
{
    public class NovaPessoaCommandInput : CommandInput<NovaPessoaCommandResult>
    {
        public NovaPessoaCommandInput(string nome, string email, byte idade)
        {
            Nome = nome;
            Email = email;
            Idade = idade;
        }

        public string Nome { get; private set; }
        public string Email { get; set; }
        public byte Idade { get; private set; }
    }
}