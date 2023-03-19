using AnalyticsServer.Models;

namespace AnalyticsServer.Contracts;

public interface IAnalyticsSendingService
{
    public Task<bool> Send(List<AnalyticsDataModel> models);
}