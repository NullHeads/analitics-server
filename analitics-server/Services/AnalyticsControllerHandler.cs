using AnalyticsServer.Contracts;
using AnalyticsServer.Enums;
using AnalyticsServer.Models;

namespace AnalyticsServer.Services;

public class AnalyticsControllerHandler : IAnalyticsControllerHandler
{
    private readonly ILogger<AnalyticsControllerHandler> _logger;
    private readonly AnalyticsRepository _analyticsRepository;

    public AnalyticsControllerHandler(ILogger<AnalyticsControllerHandler> logger, AnalyticsRepository analyticsRepository)
    {
        _logger = logger;
        _analyticsRepository = analyticsRepository;
    }

    public async Task<RequestResult<UserModel>> AddAnalyticsData(long userId, AnalyticsDataModel model)
    {
        if (userId == 0) return new RequestResult<UserModel>(result: false, errorCode: ErrorCode.UserIdNotAllowed);
        try
        {
            var userModel = await _analyticsRepository.UpdateAnalytics(userId, model);
            return new RequestResult<UserModel>(data: userModel);
        }
        catch (Exception e)
        {
            _logger.LogWarning("AddAnalyticsData error {Exception}", e);
            return new RequestResult<UserModel>(result: false, errorCode: ErrorCode.UnexpectedError);
        }
    }
}