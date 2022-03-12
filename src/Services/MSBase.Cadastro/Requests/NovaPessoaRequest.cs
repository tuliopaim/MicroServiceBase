namespace MSBase.Cadastro.API.Requests
{
    public class NovaPessoaRequest
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public byte Idade { get; set; }
    }
}