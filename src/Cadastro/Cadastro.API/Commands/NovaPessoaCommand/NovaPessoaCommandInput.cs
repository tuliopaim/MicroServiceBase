using Core.Application.Commands;

namespace Cadastro.API.Commands.NovaPessoaCommand
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