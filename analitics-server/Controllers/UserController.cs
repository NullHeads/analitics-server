using AnalyticsServer.Contracts;
using AnalyticsServer.Models;
using AnalyticsServer.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AnalyticsServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserControllerHandler _userControllerHandler;

    public UserController(IUserControllerHandler userControllerHandler)
    {
        _userControllerHandler = userControllerHandler;
    }

    [HttpGet]
    public async Task<RequestResult<IEnumerable<UserModelDto>>> Get(int limit = 20, int offset = 0)
    {
        return await _userControllerHandler.Get(limit, offset);
    }

    [HttpGet("{id}")]
    public async Task<RequestResult<UserModel>> GetById([FromRoute] long id)
    {
        return await _userControllerHandler.GetById(id);
    }

    [HttpPost]
    public async Task<RequestResult<UserModel>> Add([FromBody] UserInsertModelDto model)
    {
        return await _userControllerHandler.Add(model);
    }

    [HttpPut("{id}")]
    public async Task<RequestResult<UserModel>> Update([FromRoute] long id, [FromBody] UserInsertModelDto model)
    {
        return await _userControllerHandler.Update(id, model);
    }

    [HttpPost("delete/{id}")]
    public async Task Remove([FromRoute] long id)
    {
        await _userControllerHandler.Remove(id);
    }
}