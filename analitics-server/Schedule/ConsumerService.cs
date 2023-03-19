using AnalyticsServer.Contracts;

namespace AnalyticsServer.Schedule;

public class ConsumerService : IHostedService
{
    private readonly ILogger<ConsumerService> _logger;
    private readonly IAnalyticsReceivingService _analyticsReceivingService;

    public ConsumerService(IAnalyticsReceivingService analyticsReceivingService, ILogger<ConsumerService> logger)
    {
        _analyticsReceivingService = analyticsReceivingService;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await Task.Factory.StartNew(async () =>
        {
            await _analyticsReceivingService.Receive(cancellationToken);
        }, cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogWarning("Stop ConsumerService");
        return Task.CompletedTask;
    }
}