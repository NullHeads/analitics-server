using AnalyticsServer.Contracts;
using AnalyticsServer.Models;
using AnalyticsServer.Models.Dto;

namespace AnalyticsServer.Services;

public class AnalyticsRepositoryMock : IAnalyticsRepository
{
    private IEnumerable<AnalyticsModel?> DataMock => new[]
    {
        new AnalyticsModel()
        {
            Id = 1,
            FirstName = "firstName1",
            LastName = "lastName1",
            JobTitle = "jobTitle1",
        },
        new AnalyticsModel()
        {
            Id = 2,
            FirstName = "firstName2",
            LastName = "lastName2",
            JobTitle = "jobTitle2",
        },
        new AnalyticsModel()
        {
            Id = 3,
            FirstName = "firstName3",
            LastName = "lastName3",
            JobTitle = "jobTitle3",
        },
    };

    public Task<IEnumerable<AnalyticsModel?>> GetList(int limit, int offset)
    {
        return Task.FromResult(DataMock.Skip(offset).Take(limit));
    }

    public Task<AnalyticsModel?> GetById(long id)
    {
        return Task.FromResult(DataMock.FirstOrDefault(it => it != null && it.Id == id));
    }

    public Task<AnalyticsModel> Add(AnalyticsModelInsertModelDto model)
    {
        throw new NotImplementedException();
    }

    public Task<AnalyticsModel> Update(long id, AnalyticsModelInsertModelDto model)
    {
        throw new NotImplementedException();
    }

    public Task Remove(long id)
    {
        throw new NotImplementedException();
    }
}