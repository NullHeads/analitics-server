using AnalyticsServer.Models;

namespace AnalyticsServer.Contracts;

public interface IAnalyticsRepository
{
    public IEnumerable<AnalyticsModelDto?> GetList();
    public AnalyticsModelDto? GetById(long id);
}