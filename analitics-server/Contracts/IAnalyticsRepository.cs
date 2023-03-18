using AnalyticsServer.Models;
using AnalyticsServer.Models.Dto;

namespace AnalyticsServer.Contracts;

public interface IAnalyticsRepository
{
    public Task<IEnumerable<AnalyticsModel?>> GetList(int limit, int offset);
    public Task<AnalyticsModel?> GetById(long id);
    public Task<AnalyticsModel> Add(AnalyticsModelInsertModelDto model);
    public Task<AnalyticsModel> Update(long id, AnalyticsModelInsertModelDto model);
    public Task Remove(long id);
}