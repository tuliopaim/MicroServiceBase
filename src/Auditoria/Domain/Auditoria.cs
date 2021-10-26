using System;
using System.Collections.Generic;
using CQRS.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace AuditoriaAPI.Domain
{
    public class Auditoria : AuditableEntity, IAuditoria
    {
        public Auditoria()
        {
        }
        
        public EntityState TipoAuditoria { get; init; }

        public Guid? IdEntidade { get; init; }

        public string NomeEntidade { get; init; }

        public string NomeTabela { get; init; }
        
        public ICollection<AuditoriaPropriedade> Propriedades { get; init; }
    }
}
