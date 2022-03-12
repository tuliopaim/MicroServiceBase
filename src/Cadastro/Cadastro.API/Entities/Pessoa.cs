using Core.Domain;

namespace Cadastro.API.Entities
{
    public class Pessoa : AuditableEntity
    {
        public Pessoa(string nome, string email, byte idade)
        {
            Nome = nome;
            Email = email;
            Idade = idade;
        }

        public string Nome { get; private set; }
        public string Email { get; private set; }
        public byte Idade { get; private set; }

        public void AlterarNome(string novoNome)
        {
            if (string.IsNullOrWhiteSpace(novoNome)) return;

            Nome = novoNome;
        }

        public void AlterarIdade(byte novaIdade)
        {
            if (novaIdade < 18) return;

            Idade = novaIdade;
        }
    }
}