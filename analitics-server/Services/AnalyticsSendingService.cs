using System.Text;
using System.Text.Json;
using AnalyticsServer.Contracts;
using AnalyticsServer.Models;
using RabbitMQ.Client;

namespace AnalyticsServer.Services;

public class AnalyticsSendingService : IAnalyticsSendingService
{
    private readonly ILogger<AnalyticsSendingService> _logger;
    private readonly ConnectionFactory _factory;

    public AnalyticsSendingService(ILogger<AnalyticsSendingService> logger, ConnectionFactory factory)
    {
        _logger = logger;
        _factory = factory;
    }

    public async Task<bool> Send(List<AnalyticsDataModel> models)
    {
        while (true)
        {
            await Task.Delay(60000);
            var data = JsonSerializer.Serialize(models);
            try
            {
                using var connection = _factory.CreateConnection();
                using var channel = connection.CreateModel();

                channel.QueueDeclare(
                    queue: "DownStream",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var body = Encoding.UTF8.GetBytes(data);

                channel.BasicPublish(
                    exchange: string.Empty,
                    routingKey: "DownStream",
                    basicProperties: null,
                    body: body);

                _logger.LogInformation("Start analytics for {Data}", data);
            }
            catch (Exception exception)
            {
                _logger.LogWarning("Error when start scanning {Data} exp {Exception}", data, exception);
            }
        }
    }
}