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
    public IEnumerable<AnalyticsModelDto?> Get()
    {
        return _analyticsRepository.GetList();
    }
    
    [HttpGet("{id}")]
    public AnalyticsModelDto? GetById([FromRoute] long id)
    {
        return _analyticsRepository.GetById(id);
    }
}