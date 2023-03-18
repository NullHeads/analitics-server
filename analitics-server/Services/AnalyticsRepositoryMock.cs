using AnalyticsServer.Contracts;
using AnalyticsServer.Models;

namespace AnalyticsServer.Services;

public class AnalyticsRepositoryMock : IAnalyticsRepository
{
    private IEnumerable<AnalyticsModelDto?> DataMock => new[]
    {
        new AnalyticsModelDto()
        {
            Id = 1,
            FirstName = "firstName1",
            LastName = "lastName1",
            JobTitle = "jobTitle1",
        },
        new AnalyticsModelDto()
        {
            Id = 2,
            FirstName = "firstName2",
            LastName = "lastName2",
            JobTitle = "jobTitle2",
        },
        new AnalyticsModelDto()
        {
            Id = 3,
            FirstName = "firstName3",
            LastName = "lastName3",
            JobTitle = "jobTitle3",
        },
    };

    public IEnumerable<AnalyticsModelDto?> GetList()
    {
        return DataMock;
    }

    public AnalyticsModelDto? GetById(long id)
    {
        return DataMock.FirstOrDefault(it => it != null && it.Id == id);
    }

    public AnalyticsModelDto Add(AnalyticsModelDto model)
    {
        throw new NotImplementedException();
    }

    public AnalyticsModelDto Update(long id, AnalyticsModelDto model)
    {
        throw new NotImplementedException();
    }

    public void Remove(long id)
    {
        throw new NotImplementedException();
    }
}