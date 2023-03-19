using AnalyticsServer.Models;

namespace AnalyticsServer.Contracts;

public interface IAnalyticsControllerHandler
{
    Task<RequestResult<UserModel>> AddAnalyticsData(long userId, AnalyticsDataModel model);
}