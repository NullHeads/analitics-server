using AnalyticsServer.Contracts;
using AnalyticsServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnalyticsServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnalyticsController : ControllerBase
{
    private readonly IAnalyticsControllerHandler _analyticsControllerHandler;

    public AnalyticsController(IAnalyticsControllerHandler analyticsControllerHandler)
    {
        _analyticsControllerHandler = analyticsControllerHandler;
    }

    [HttpPost]
    public async Task<RequestResult<UserModel>> AddAnalyticsData([FromQuery] long userId, [FromBody] AnalyticsDataModel model)
    {
        return await _analyticsControllerHandler.AddAnalyticsData(userId, model);
    }
}