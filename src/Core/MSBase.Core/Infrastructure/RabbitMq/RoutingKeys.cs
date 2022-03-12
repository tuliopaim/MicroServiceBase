namespace MSBase.Core.Infrastructure.RabbitMq;

public static class RoutingKeys
{   
    public const string NovaAuditoria = "NovaAuditoria";    
    public const string NovoEmail = "NovoEmail";

    public static string[] All => new []
    {
        NovaAuditoria,
        NovoEmail
    };
}