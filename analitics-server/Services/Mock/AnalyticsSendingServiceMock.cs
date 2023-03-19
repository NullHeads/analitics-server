using AnalyticsServer.Contracts;
using AnalyticsServer.Models.Dto;

namespace AnalyticsServer.Services.Mock;

public class AnalyticsSendingServiceMock : IAnalyticsSendingService
{
    public Task<bool> Send(List<AnalyticsDataModelDto> models)
    {
        throw new NotImplementedException();
    }
}