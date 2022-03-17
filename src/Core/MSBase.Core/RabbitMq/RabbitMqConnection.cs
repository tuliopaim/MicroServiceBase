using MSBase.Core.API;
using RabbitMQ.Client;

namespace MSBase.Core.RabbitMq;

public class RabbitMqConnection
{
    private readonly IEnvironment _environment;
    private IConnection _conexao;

    public RabbitMqConnection(IEnvironment environment)
    {
        _environment = environment;
        
        CriarConexao();
    }

    public IConnection Connection => ExisteConexao() ? _conexao : null;

    private void CriarConexao()
    {
        var hostName = _environment["RabbitMqSettings:HostName"];
        var port = _environment["RabbitMqSettings:Port"];
        var userName = _environment["RabbitMqSettings:UserName"];
        var password = _environment["RabbitMqSettings:Password"];

        var factory = new ConnectionFactory
        {
            HostName = hostName,
            Port = Convert.ToInt32(port),
            UserName = userName,
            Password = password,
            DispatchConsumersAsync = true,
        };

        _conexao = factory.CreateConnection();
    }

    private bool ExisteConexao()
    {
        if (_conexao != null)
        {
            return true;
        }

        CriarConexao();

        return _conexao != null;
    }
}
