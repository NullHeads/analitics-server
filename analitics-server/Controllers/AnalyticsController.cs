using AnalyticsServer.Contracts;
using AnalyticsServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnalyticsServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnalyticsController : ControllerBase
{
    private readonly IAnalyticsRepository _analyticsRepository;

    public AnalyticsController(IAnalyticsRepository analyticsRepository)
    {
        _analyticsRepository = analyticsRepository;
    }

    [HttpPost]
    public async Task<UserModel?> AddAnalyticsData([FromQuery] long userId, [FromBody] AnalyticsDataModel model)
    {
        return await _analyticsRepository.UpdateAnalytics(userId, model);
    }
}