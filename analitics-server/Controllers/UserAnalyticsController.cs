using AnalyticsServer.Contracts;
using AnalyticsServer.Models;
using AnalyticsServer.Models.Dto;
using AutoMapper;
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
        var list = await _analyticsRepository.GetList(limit, offset);
        return list;
    }

    [HttpGet("{id}")]
    public async Task<AnalyticsModel?> GetById([FromRoute] long id)
    {
        return await _analyticsRepository.GetById(id);
    }

    [HttpPost]
    public async Task<AnalyticsModel?> Add([FromBody] AnalyticsModelInsertModelDto model)
    {
        return await _analyticsRepository.Add(model);
    }

    [HttpPut("{id}")]
    public async Task<AnalyticsModel?> Update([FromRoute] long id, [FromBody] AnalyticsModelInsertModelDto model)
    {
        return await _analyticsRepository.Update(id, model);
    }

    [HttpPost("delete/{id}")]
    public async Task Remove([FromRoute] long id)
    {
        await _analyticsRepository.Remove(id);
    }
}