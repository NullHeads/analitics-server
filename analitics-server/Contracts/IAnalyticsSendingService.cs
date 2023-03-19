using AnalyticsServer.Models;
using AnalyticsServer.Models.Dto;

namespace AnalyticsServer.Contracts;

public interface IAnalyticsSendingService
{
    public Task<bool> Send(List<AnalyticsDataModelDto?> models);
}