using AnalyticsServer.Contracts;
using AnalyticsServer.Models;
using Redis.OM;
using Redis.OM.Searching;

namespace AnalyticsServer.Services;

public class AnalyticsRepository : IAnalyticsRepository
{
    private readonly RedisCollection<UserModel> _people;

    public AnalyticsRepository(RedisConnectionProvider provider)
    {
        _people = (RedisCollection<UserModel>)provider.RedisCollection<UserModel>();
    }

    public async Task<IEnumerable<UserModel?>> GetList()
    {
        return await _people.ToListAsync();
    }

    public async Task<UserModel?> GetById(long id)
    {
        return await _people.FirstOrDefaultAsync(it => it.Id == id);
    }

    public async Task<UserModel> Add(UserModel model)
    {
        await _people.InsertAsync(model);
        return model;
    }

    public async Task<UserModel> Update(long id, UserModel model)
    {
        var person = await _people.FirstAsync(x => x.Id == id);
        person.FirstName = model.FirstName;
        person.JobTitle = model.JobTitle;
        person.LastName = model.LastName;
        person.BurnoutPercent = model.BurnoutPercent;
        if (model.AnalyticsData is not null)
            person.AnalyticsData = model.AnalyticsData;
        await Remove(id);
        await Add(person);
        return person;
    }

    public async Task Remove(long id)
    {
        var person = await GetById(id);
        if (person != null) await _people.DeleteAsync(person);
    }

    public async Task<int> GetNewId()
    {
        // TODO: crutch, think about new Id in redis
        var list = await _people.ToListAsync();
        if (!list.Any()) 
            return 1;
        
        return list.Max(it => it.Id) + 1;
    }
}