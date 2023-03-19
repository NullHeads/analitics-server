using AnalyticsServer.Models;
using AnalyticsServer.Models.Dto;

namespace AnalyticsServer.Contracts;

public interface IUserControllerHandler
{
    Task<RequestResult<IEnumerable<UserModelDto>>> Get(int limit, int offset);
    Task<RequestResult> Remove(long id);
    Task<RequestResult<UserModel>> Update(long id, UserInsertModelDto model);
    Task<RequestResult<UserModel>> Add(UserInsertModelDto model);
    Task<RequestResult<UserModel>> GetById(long id);
}