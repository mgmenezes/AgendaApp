using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

public class ContatoConsumer : BackgroundService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly ILogger<ContatoConsumer> _logger;

    public ContatoConsumer(IConfiguration configuration, ILogger<ContatoConsumer> logger)
    {
        var factory = new ConnectionFactory
        {
            HostName = configuration["RabbitMQ:Host"],
            Port = int.Parse(configuration["RabbitMQ:Port"]),
            UserName = configuration["RabbitMQ:Username"],
            Password = configuration["RabbitMQ:Password"]
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _logger = logger;

        _channel.QueueDeclare(
            queue: "contatos.criado",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            _logger.LogInformation($"Mensagem recebida: {message}");


            _channel.BasicAck(ea.DeliveryTag, false);
        };

        _channel.BasicConsume(
            queue: "contatos.criado",
            autoAck: false,
            consumer: consumer
        );

        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
        base.Dispose();
    }
}