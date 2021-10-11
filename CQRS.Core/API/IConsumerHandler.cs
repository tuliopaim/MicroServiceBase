using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Core.API
{
    public interface IConsumerHandler
    {
        Task Handle(CancellationToken cancellationToken);
    }
}