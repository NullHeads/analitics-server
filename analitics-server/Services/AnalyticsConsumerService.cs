using System.Text;
using AnalyticsServer.Contracts;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace AnalyticsServer.Services;

public class AnalyticsConsumerService : IAnalyticsReceivingService
{
    private readonly ILogger<AnalyticsConsumerService> _logger;
    private readonly ConnectionFactory _factory;

    public AnalyticsConsumerService(ILogger<AnalyticsConsumerService> logger, ConnectionFactory factory)
    {
        _logger = logger;
        _factory = factory;
    }

    public Task Receive(CancellationToken cancellationToken)
    {
        
        while (!cancellationToken.IsCancellationRequested)
        {
            using var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: "UpStream",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                _logger.LogInformation("Receive new data {Data}", message);
            };
            channel.BasicConsume(
                queue: "UpStream",
                autoAck: true,
                consumer: consumer);

            Console.WriteLine(" Press [enter] to exit.");
        }
        
        return Task.CompletedTask;
    }
}