using Microsoft.EntityFrameworkCore;
using MSBase.Core.Domain;

namespace MSBase.Auditoria.API.Domain;

public class AuditoriaEntidade : AuditableEntity, IAuditoria
{
    public EntityState TipoAuditoria { get; init; }

    public Guid? IdEntidade { get; init; }

    public string NomeEntidade { get; init; }

    public string NomeTabela { get; init; }

    public ICollection<AuditoriaPropriedade> Propriedades { get; init; }
}