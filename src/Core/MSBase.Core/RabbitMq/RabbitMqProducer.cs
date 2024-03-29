﻿using System.Text;
using Microsoft.Extensions.Logging;
using MSBase.Core.Extensions;
using MSBase.Core.RabbitMq.Messages;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace MSBase.Core.RabbitMq;

public class RabbitMqProducer
{
    private readonly RabbitMqConnection _rabbitConexao;
    private readonly ILogger<RabbitMqProducer> _logger;
    private readonly IModel _channel;

    public RabbitMqProducer(RabbitMqConnection rabbitConexao, ILogger<RabbitMqProducer> logger)
    {
        _rabbitConexao = rabbitConexao;
        _logger = logger;
        _channel = Conexao.CreateModel();
    }

    private IConnection Conexao => _rabbitConexao.Connection;

    public bool Publish<TMessage>(TMessage message, MessageType messageType, string routingKey)
    {
        var serializedMessage = JsonConvert.SerializeObject(message);

        var messageBase = new RabbitMessage(serializedMessage, messageType);

        return PublishMessage(JsonConvert.SerializeObject(messageBase), routingKey);
    }

    private bool PublishMessage(string serializedMessage, string routingKey)
    {
        try
        {
            if (Conexao is null) return false;

            var mensagemBytes = Encoding.UTF8.GetBytes(serializedMessage);

            var propriedades = _channel.CreateBasicProperties();
            propriedades.Persistent = true;
            propriedades.CreateRetryCountHeader();

            _channel.BasicPublish("",
                routingKey,
                propriedades,
                mensagemBytes);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception captured on {Method} - {@DataPublished}",
                nameof(PublishMessage), new { serializedMessage, routingKey });

            return false;
        }
    }
}
