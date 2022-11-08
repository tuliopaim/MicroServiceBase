namespace MSBase.Core.RabbitMq;

public class RabbitMqSettings
{
    public string HostName { get; set; }
    public string UserName { get; set; }
    public int Port { get; set; }
    public string Password { get; set; }
    public RabbitMqRetrySettings RetrySettings { get; set; }
}

public class RabbitMqRetrySettings
{
    public int Count { get; set; }
    public int DurationInSeconds { get; set; }
}
