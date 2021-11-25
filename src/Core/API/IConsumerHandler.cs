namespace MSBase.Core.API
{
    public interface IConsumerHandler
    {
        Task Handle(CancellationToken cancellationToken);
    }
}