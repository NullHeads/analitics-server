using AnalyticsServer.Contracts;
using AnalyticsServer.Models;
using Redis.OM;
using Redis.OM.Searching;

namespace AnalyticsServer.Services;

public class AnalyticsRepository : IAnalyticsRepository
{
    private readonly RedisCollection<AnalyticsModel> _people;

    public AnalyticsRepository(RedisConnectionProvider provider)
    {
        _people = (RedisCollection<AnalyticsModel>)provider.RedisCollection<AnalyticsModel>();
    }

    public async Task<IEnumerable<AnalyticsModel?>> GetList(int limit, int offset)
    {
        return await _people.ToListAsync();
    }

    public async Task<AnalyticsModel?> GetById(long id)
    {
        return await _people.FirstOrDefaultAsync(it => it.Id == id);
    }

    public async Task<AnalyticsModel> Add(AnalyticsModel model)
    {
        await _people.InsertAsync(model);
        return model;
    }

    public async Task<AnalyticsModel> Update(long id, AnalyticsModel model)
    {
        var person = await _people.FirstAsync(x => x.Id == id);
        person.FirstName = model.FirstName;
        person.JobTitle = model.JobTitle;
        person.LastName = model.LastName;
        await _people.SaveAsync();
        return person;
    }

    public async Task Remove(long id)
    {
        var person = await GetById(id);
        if (person != null) await _people.DeleteAsync(person);
    }
}