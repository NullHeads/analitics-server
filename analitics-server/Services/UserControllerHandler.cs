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
        var config = new MapperConfiguration(cfg => cfg.CreateMap<UserModel, UserModelDto>());
        var mapper = config.CreateMapper();
        try
        {
            var list = await _analyticsRepository.GetList();
            return new RequestResult<IEnumerable<UserModelDto>>(
                data: list.ToList().Skip(offset).Take(limit).Select(it => mapper.Map<UserModelDto>(it))
            );
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
            var user = await _analyticsRepository.GetById(id);
            return user is null
                ? new RequestResult<UserModel>(false, ErrorCode.UserNotFound)
                : new RequestResult<UserModel>(data: user);
        }
        catch (Exception e)
        {
            _logger.LogWarning("UserControllerHandler GetById Error {Exception}", e);
            return new RequestResult<UserModel>(false, ErrorCode.UnexpectedError);
        }
    }

    public async Task<RequestResult<UserModel>> Add(UserInsertModelDto model)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<UserInsertModelDto, UserModel>());
        var mapper = config.CreateMapper();
        try
        {
            var newModel = mapper.Map<UserModel>(model);
            newModel.Id = await _analyticsRepository.GetNewId();

            return new RequestResult<UserModel>(data: await _analyticsRepository.Add(newModel));
        }
        catch (Exception e)
        {
            _logger.LogWarning("UserControllerHandler Add Error {Exception}", e);
            return new RequestResult<UserModel>(false, ErrorCode.UnexpectedError);
        }
    }

    public async Task<RequestResult<UserModel>> Update(long id, UserInsertModelDto model)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<UserInsertModelDto, UserModel>());
        var mapper = config.CreateMapper();
        try
        {
            var user = await _analyticsRepository.GetById(id);
            if (user is null)
                return new RequestResult<UserModel>(false, ErrorCode.UserNotFound); 
            
            var newModel = mapper.Map<UserModel>(model);
            return new RequestResult<UserModel>(data: await _analyticsRepository.Update(id, newModel));
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