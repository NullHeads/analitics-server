using AnalyticsServer.Models;

namespace AnalyticsServer.Contracts;

public interface IAnalyticsRepository
{
    public Task<IEnumerable<AnalyticsModelDto?>> GetList(int limit, int offset);
    public Task<AnalyticsModelDto?> GetById(long id);
    public Task<AnalyticsModelDto> Add(AnalyticsModelDto model);
    public Task<AnalyticsModelDto> Update(long id, AnalyticsModelDto model);
    public Task Remove(long id);
}