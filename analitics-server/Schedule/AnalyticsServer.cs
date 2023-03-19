using AnalyticsServer.Contracts;
using AnalyticsServer.Models;

namespace AnalyticsServer.Schedule;

public class AnalyticsServer : IHostedService
{
    private readonly ILogger<AnalyticsServer> _logger;
    private readonly IAnalyticsSendingService _analyticsSendingService;
    private Timer? _timer;


    public AnalyticsServer(ILogger<AnalyticsServer> logger, IAnalyticsSendingService analyticsSendingService)
    {
        _logger = logger;
        _analyticsSendingService = analyticsSendingService;
    }

    private async Task Shedule()
    {
        
    }

    private void DoWork(object? state)
    {
        Task.Run(Shedule);
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Analytics server running");
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(10));
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogWarning("AnalyticsServer is stoped");
        return Task.CompletedTask;
    }
}