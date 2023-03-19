using AnalyticsServer.Models;


namespace AnalyticsServer.Contracts;

public interface IAnalyticsRepository
{
    public Task<IEnumerable<UserModel?>> GetList();
    public Task<UserModel?> GetById(long id);
    public Task<UserModel> Add(UserModel model);
    public Task<UserModel> Update(long id, UserModel model);
    public Task Remove(long id);
    Task<int> GetNewId();
}