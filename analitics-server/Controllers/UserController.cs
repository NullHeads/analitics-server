﻿
using AnalyticsServer.Contracts;
using AnalyticsServer.Models;
using AnalyticsServer.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AnalyticsServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IAnalyticsRepository _analyticsRepository;

    public UserController(IAnalyticsRepository analyticsRepository)
    {
        _analyticsRepository = analyticsRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<UserModel?>> Get(int limit = 20, int offset = 0)
    {
        var list = await _analyticsRepository.GetList(limit, offset);
        return list;
    }

    [HttpGet("{id}")]
    public async Task<UserModel?> GetById([FromRoute] long id)
    {
        return await _analyticsRepository.GetById(id);
    }

    [HttpPost]
    public async Task<UserModel?> Add([FromBody] UserInsertModelDto model)
    {
        return await _analyticsRepository.Add(model);
    }

    [HttpPut("{id}")]
    public async Task<UserModel?> Update([FromRoute] long id, [FromBody] UserInsertModelDto model)
    {
        return await _analyticsRepository.Update(id, model);
    }

    [HttpPost("delete/{id}")]
    public async Task Remove([FromRoute] long id)
    {
        await _analyticsRepository.Remove(id);
    }
}