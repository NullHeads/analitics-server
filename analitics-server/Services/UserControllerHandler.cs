using AnalyticsServer.Contracts;
using AnalyticsServer.Enums;
using AnalyticsServer.Models;
using AnalyticsServer.Models.Dto;
using AutoMapper;

namespace AnalyticsServer.Services;

public class UserControllerHandler : IUserControllerHandler
{
    private readonly ILogger<UserControllerHandler> _logger;
    private readonly IAnalyticsRepository _analyticsRepository;

    public UserControllerHandler(IAnalyticsRepository analyticsRepository, ILogger<UserControllerHandler> logger)
    {
        _analyticsRepository = analyticsRepository;
        _logger = logger;
    }

    public async Task<RequestResult<IEnumerable<UserModelDto>>> Get(int limit = 20, int offset = 0)
    {
        try
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UserModel, UserModelDto>());
            var mapper = config.CreateMapper();
            var list = await _analyticsRepository.GetList();
            return new RequestResult<IEnumerable<UserModelDto>>(
                data: list.ToList().Skip(offset).Take(limit).Select(it => mapper.Map<UserModelDto>(it)));
        }
        catch (Exception e)
        {
            _logger.LogWarning("UserControllerHandler Get List Error {Exception}", e);
            return new RequestResult<IEnumerable<UserModelDto>>(false, ErrorCode.UnexpectedError);
        }
    }

    public async Task<RequestResult<UserModel>> GetById(long id)
    {
        try
        {
            return new RequestResult<UserModel>(data: await _analyticsRepository.GetById(id));
        }
        catch (Exception e)
        {
            _logger.LogWarning("UserControllerHandler GetById Error {Exception}", e);
            return new RequestResult<UserModel>(false, ErrorCode.UnexpectedError);
        }
    }

    public async Task<RequestResult<UserModel>> Add(UserInsertModelDto model)
    {
        try
        {
            return new RequestResult<UserModel>(data: await _analyticsRepository.Add(model));
        }
        catch (Exception e)
        {
            _logger.LogWarning("UserControllerHandler Add Error {Exception}", e);
            return new RequestResult<UserModel>(false, ErrorCode.UnexpectedError);
        }
    }

    public async Task<RequestResult<UserModel>> Update(long id, UserInsertModelDto model)
    {
        try
        {
            return new RequestResult<UserModel>(data: await _analyticsRepository.Update(id, model));
        }
        catch (Exception e)
        {
            _logger.LogWarning("UserControllerHandler Update {Exception}", e);
            return new RequestResult<UserModel>(false, ErrorCode.UnexpectedError);
        }
    }

    public async Task<RequestResult> Remove(long id)
    {
        try
        {
            await _analyticsRepository.Remove(id);
            return new RequestResult();
        }
        catch (Exception e)
        {
            _logger.LogWarning("UserControllerHandler Remove {Exception}", e);
            return new RequestResult(false, ErrorCode.UnexpectedError);
        }
    }
}