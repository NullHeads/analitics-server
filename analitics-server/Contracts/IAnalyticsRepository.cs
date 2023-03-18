using AnalyticsServer.Models;

namespace AnalyticsServer.Contracts;

public interface IAnalyticsRepository
{
    public IEnumerable<AnalyticsModelDto?> GetList();
    public AnalyticsModelDto? GetById(long id);
    public AnalyticsModelDto Add(AnalyticsModelDto model);
    public AnalyticsModelDto Update(long id, AnalyticsModelDto model);
    public void Remove(long id);
}