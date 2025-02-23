using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

using AgendaApp.Application.MessageBus.Interfaces;

public class RabbitMQService : IMessageBusService, IDisposable
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly IConfiguration _configuration;

    public RabbitMQService(IConfiguration configuration)
    {
        _configuration = configuration;
        var factory = new ConnectionFactory
        {
            HostName = _configuration["RabbitMQ:Host"],
            Port = int.Parse(_configuration["RabbitMQ:Port"]),
            UserName = _configuration["RabbitMQ:Username"],
            Password = _configuration["RabbitMQ:Password"]
        };

        try
        {
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }
        catch (Exception ex)
        {
            // Log do erro
            throw;
        }
    }

    public void PublishMessage<T>(string queue, T message)
    {
        _channel.QueueDeclare(
            queue: queue,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        _channel.BasicPublish(
            exchange: "",
            routingKey: queue,
            basicProperties: null,
            body: body
        );
    }

    public void Subscribe<T>(string queue, Action<T> onMessage)
    {
        _channel.QueueDeclare(
            queue: queue,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var messageObject = JsonSerializer.Deserialize<T>(message);
            onMessage(messageObject);
            _channel.BasicAck(ea.DeliveryTag, false);
        };

        _channel.BasicConsume(
            queue: queue,
            autoAck: false,
            consumer: consumer
        );
    }

    public void Dispose()
    {
        if (_channel.IsOpen)
            _channel.Close();
        if (_connection.IsOpen)
            _connection.Close();
    }
}