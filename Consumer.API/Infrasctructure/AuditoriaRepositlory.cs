using AuditoriaAPI.Domain;
using CQRS.Core.Infrastructure;

namespace AuditoriaAPI.Infrasctructure
{
    public class AuditoriaRepository : GenericRepository<Auditoria>, IAuditoriaRepository
    {
        public AuditoriaRepository(AuditoriaDbContext context) : base(context)
        {
        }
    }
}
