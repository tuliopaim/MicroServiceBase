using System;
using System.Threading;
using System.Threading.Tasks;

namespace MSBase.Core.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}