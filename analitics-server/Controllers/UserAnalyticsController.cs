using AnalyticsServer.Contracts;
using AnalyticsServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnalyticsServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserAnalyticsController : ControllerBase
{
    private readonly ILogger<UserAnalyticsController> _logger;
    private readonly IAnalyticsRepository _analyticsRepository;

    public UserAnalyticsController(ILogger<UserAnalyticsController> logger, IAnalyticsRepository analyticsRepository)
    {
        _logger = logger;
        _analyticsRepository = analyticsRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<AnalyticsModel?>> Get(int limit = 20, int offset = 0)
    {
        return await _analyticsRepository.GetList(limit, offset);
    }

    [HttpGet("{id}")]
    public async Task<AnalyticsModel?> GetById([FromRoute] long id)
    {
        return await _analyticsRepository.GetById(id);
    }
}