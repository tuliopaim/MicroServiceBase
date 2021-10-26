using System;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Core.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}