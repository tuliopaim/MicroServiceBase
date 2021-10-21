using System;

namespace CQRS.API.Requests
{
    public class EditarPessoaRequest
    {
        public Guid PessoaId { get; set; }
        public byte NovaIdade { get; set; }
    }
}