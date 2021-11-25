using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Auditoria.API.Domain
{
    public class Auditoria : AuditableEntity, IAuditoria
    {
        public EntityState TipoAuditoria { get; init; }

        public Guid? IdEntidade { get; init; }

        public string NomeEntidade { get; init; }

        public string NomeTabela { get; init; }
        
        public ICollection<AuditoriaPropriedade> Propriedades { get; init; }
    }
}
