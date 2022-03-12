using MSBase.Auditoria.API.Domain;
using MSBase.Core.Infrastructure;

namespace MSBase.Auditoria.API.Infrasctructure
{
    public class AuditoriaEntidadeRepository : GenericRepository<AuditoriaEntidade>, IAuditoriaRepository
    {
        public AuditoriaEntidadeRepository(AuditoriaDbContext context) : base(context)
        {
        }
    }
}
