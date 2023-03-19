
using AnalyticsServer.Models;
using AnalyticsServer.Models.Dto;


namespace AnalyticsServer.Contracts;

public interface IAnalyticsRepository
{
    public Task<IEnumerable<UserModel?>> GetList(int limit, int offset);
    public Task<UserModel?> GetById(long id);
    public Task<UserModel> Add(UserInsertModelDto model);
    public Task<UserModel> Update(long id, UserInsertModelDto model);
    public Task Remove(long id);
}