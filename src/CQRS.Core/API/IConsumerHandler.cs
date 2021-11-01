using System.Threading;
using System.Threading.Tasks;

namespace MSBase.Core.API
{
    public interface IConsumerHandler
    {
        Task Handle(CancellationToken cancellationToken);
    }
}