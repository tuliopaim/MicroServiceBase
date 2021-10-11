using BenchmarkDotNet.Running;

namespace CQRS.BenchmarkConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            BenchmarkRunner.Run<BenchmarkHandleMediatorResult>();
        }
    }
}
