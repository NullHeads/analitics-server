using AnalyticsServer.Contracts;
using AnalyticsServer.Models;

namespace AnalyticsServer.Services.Mock;

public class AnalyticsSendingServiceMock : IAnalyticsSendingService
{
    public Task<bool> Send(List<AnalyticsDataModel> models)
    {
        throw new NotImplementedException();
    }
}