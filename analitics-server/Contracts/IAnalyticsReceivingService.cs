namespace AnalyticsServer.Contracts;

public interface IAnalyticsReceivingService
{
    public Task Receive(CancellationToken cancellationToken);
}