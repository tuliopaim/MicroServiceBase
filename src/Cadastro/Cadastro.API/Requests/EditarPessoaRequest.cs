namespace Cadastro.API.Requests
{
    public class EditarPessoaRequest
    {
        public Guid PessoaId { get; set; }
        public byte NovaIdade { get; set; }
    }
}