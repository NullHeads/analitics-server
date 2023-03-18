using AnalyticsServer.Models;

namespace AnalyticsServer.Contracts;

public interface IAnalyticsRepository
{
    public Task<IEnumerable<AnalyticsModel?>> GetList(int limit, int offset);
    public Task<AnalyticsModel?> GetById(long id);
    public Task<AnalyticsModel> Add(AnalyticsModel model);
    public Task<AnalyticsModel> Update(long id, AnalyticsModel model);
    public Task Remove(long id);
}