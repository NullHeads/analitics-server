using AnalyticsServer.Contracts;

namespace AnalyticsServer.Schedule;

public class AnalyticsServer : IHostedService
{
    private readonly ILogger<AnalyticsServer> _logger;
    private readonly IAnalyticsSendingService _analyticsSendingService;
    private readonly IAnalyticsRepository _analyticsRepository;
    private Timer? _timer;


    public AnalyticsServer(ILogger<AnalyticsServer> logger, IAnalyticsSendingService analyticsSendingService,
        IAnalyticsRepository analyticsRepository)
    {
        _logger = logger;
        _analyticsSendingService = analyticsSendingService;
        _analyticsRepository = analyticsRepository;
    }

    private async Task Shedule()
    {
        var data = await _analyticsRepository.GetList();
        await _analyticsSendingService.Send(data.Select(it => it?.GetAnalyticsData()).ToList());
    }

    private void DoWork(object? state)
    {
        Task.Run(Shedule);
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Analytics server running");
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromDays(1));
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogWarning("AnalyticsServer is stoped");
        return Task.CompletedTask;
    }
}