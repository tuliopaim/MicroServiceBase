namespace CQRS.API.Requests
{
    public class NovaPessoaRequest
    {
        public string Nome { get; set; }
        public ushort Idade { get; set; }
    }
}