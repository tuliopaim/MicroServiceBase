namespace CQRS.Core.Infrastructure.Auditoria
{
    public class NovaAuditoriaPropriedadeDto
    {
        public string NomeDaPropriedade { get; set; }
        public string NomeDaColuna { get; set; }
        public string ValorAntigo { get; set; }
        public string ValorNovo { get; set; }
        public bool EhChavePrimaria { get; set; }

    }
}
