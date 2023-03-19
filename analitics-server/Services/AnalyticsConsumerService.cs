using System.Text;
using System.Text.Json;
using AnalyticsServer.Contracts;
using AnalyticsServer.Models.Dto;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace AnalyticsServer.Services;

public class AnalyticsConsumerService : IAnalyticsReceivingService
{
    private readonly ILogger<AnalyticsConsumerService> _logger;
    private readonly ConnectionFactory _factory;
    private readonly IAnalyticsRepository _analyticsRepository;

    public AnalyticsConsumerService(ILogger<AnalyticsConsumerService> logger, ConnectionFactory factory,
        IAnalyticsRepository analyticsRepository)
    {
        _logger = logger;
        _factory = factory;
        _analyticsRepository = analyticsRepository;
    }

    private async Task UpdateBurnoutPercent(List<AnalyticsResultDto>? data)
    {
        if (data is null)
            return;
        foreach (var item in data)
        {
            var user = await _analyticsRepository.GetById(item.Id);
            if (user is null)
                continue;
            user.BurnoutPercent = item.BurnoutPercent;
            await _analyticsRepository.Update(user.Id, user);
        }
    }

    public async Task Receive(CancellationToken cancellationToken)
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

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                _logger.LogInformation("Receive new data {Data}", message);
                var data = JsonSerializer.Deserialize<List<AnalyticsResultDto>>(message);
                await UpdateBurnoutPercent(data);
            };
            channel.BasicConsume(
                queue: "UpStream",
                autoAck: true,
                consumerTag: "UpStream",
                consumer: consumer);
            await Task.Delay(1000, cancellationToken);
        }
    }
}