using AnalyticsServer.Contracts;
using AnalyticsServer.Models;
using AnalyticsServer.Models.Dto;

namespace AnalyticsServer.Services.Mock;

public class AnalyticsRepositoryMock : IAnalyticsRepository
{
    private IEnumerable<UserModel?> DataMock;

    public AnalyticsRepositoryMock()
    {
        DataMock = new[]
        {
            new UserModel()
            {
                Id = 1,
                FirstName = "firstName1",
                LastName = "lastName1",
                JobTitle = "jobTitle1",
            },
            new UserModel()
            {
                Id = 2,
                FirstName = "firstName2",
                LastName = "lastName2",
                JobTitle = "jobTitle2",
            },
            new UserModel()
            {
                Id = 3,
                FirstName = "firstName3",
                LastName = "lastName3",
                JobTitle = "jobTitle3",
            },
        };
        ;
    }

    public Task<IEnumerable<UserModel?>> GetList(int limit, int offset)
    {
        return Task.FromResult(DataMock.Skip(offset).Take(limit));
    }

    public Task<UserModel?> GetById(long id)
    {
        return Task.FromResult(DataMock.FirstOrDefault(it => it != null && it.Id == id));
    }

    public Task<UserModel> Add(UserInsertModelDto model)
    {
        throw new NotImplementedException();
    }

    public Task<UserModel> Update(long id, UserInsertModelDto model)
    {
        var person = DataMock.FirstOrDefault(x => x?.Id == id);
        if (person is null) throw new NullReferenceException();
        person.FirstName = model.FirstName;
        person.JobTitle = model.JobTitle;
        person.LastName = model.LastName;
        return Task.FromResult(person);
    }

    public Task Remove(long id)
    {
        throw new NotImplementedException();
    }
}