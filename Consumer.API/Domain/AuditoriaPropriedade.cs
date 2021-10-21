using CQRS.Core.Domain;
using System;

namespace AuditoriaAPI.Domain
{
    public class AuditoriaPropriedade : AuditableEntity
    {
        protected AuditoriaPropriedade()
        {
        }

        public Guid AuditoriaId { get; init; }
        public string NomeDaPropriedade { get; init; }
        public string NomeDaColuna { get; init; }
        public string ValorAntigo { get; init; }
        public string ValorNovo { get; init; }
        public bool EhChavePrimaria { get; init; }
        public virtual Auditoria Auditoria { get; init; }
    }
}
