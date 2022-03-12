using MSBase.Core.Domain;

namespace MSBase.Auditoria.API.Domain
{
    public class AuditoriaPropriedade : AuditableEntity, IAuditoriaPropriedade
    {
        public AuditoriaPropriedade()
        {
        }

        public string NomeDaPropriedade { get; init; }
        public string NomeDaColuna { get; init; }
        public string ValorAntigo { get; init; }
        public string ValorNovo { get; init; }
        public bool EhChavePrimaria { get; init; }

        public Guid AuditoriaId { get; private set; }
        public virtual Auditoria Auditoria { get; private set; }
    }
}
