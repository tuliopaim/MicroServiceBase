using Microsoft.EntityFrameworkCore;

namespace MSBase.Core.Infrastructure.Auditoria
{
    public class NovaAuditoriaDto
    {
        public EntityState TipoAuditoria { get; set; }

        public Guid? IdEntidade { get; set; }

        public string NomeEntidade { get; set; }

        public string NomeTabela { get; set; }

        public ICollection<NovaAuditoriaPropriedadeDto> Propriedades { get; set; }
            = new List<NovaAuditoriaPropriedadeDto>();

        public bool EhAdicionado => TipoAuditoria == EntityState.Added;
        public bool EhModificado => TipoAuditoria == EntityState.Modified;
        public bool EhDeletado => TipoAuditoria == EntityState.Deleted;
    }
}
