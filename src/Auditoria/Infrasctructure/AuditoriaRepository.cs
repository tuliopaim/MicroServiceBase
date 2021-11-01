using MSBase.Core.Infrastructure;

namespace Auditoria.API.Infrasctructure
{
    public class AuditoriaRepository : GenericRepository<Domain.Auditoria>, IAuditoriaRepository
    {
        public AuditoriaRepository(AuditoriaDbContext context) : base(context)
        {
        }
    }
}
