using AnalyticsServer.Contracts;
using AnalyticsServer.Models;
using AnalyticsServer.Models.Dto;
using AutoMapper;
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

    public async Task<IEnumerable<UserModel?>> GetList(int limit, int offset)
    {
        return await _people.ToListAsync();
    }

    public async Task<UserModel?> GetById(long id)
    {
        return await _people.FirstOrDefaultAsync(it => it.Id == id);
    }

    public async Task<UserModel> Add(UserInsertModelDto model)
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<UserInsertModelDto, UserModel>());
        var mapper = config.CreateMapper();
        var newModel = mapper.Map<UserModel>(model);
        newModel.Id = await GetNewId();
        await _people.InsertAsync(newModel);
        return newModel;
    }

    public async Task<UserModel> Update(long id, UserInsertModelDto model)
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

    private async Task<int> GetNewId()
    {
        var list = await _people.ToListAsync();
        return list.Max(it => it.Id) + 1;
    }
}