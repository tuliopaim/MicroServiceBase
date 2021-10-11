using System;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Core.API;

namespace CQRS.API.Consumers.NovaPessoaConsumer2
{
    public class NovaPessoaConsumer2 //: IConsumerHandler
    {
        public async Task Handle(CancellationToken cancellationToken)
        {
            var count = 0;
            while (!cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine($"{nameof(NovaPessoaConsumer2)} ONLINE - {DateTime.Now:T}");

                await Task.Delay(1000, cancellationToken);

                if (++count > 5) throw new Exception();
            }
        }
    }
}