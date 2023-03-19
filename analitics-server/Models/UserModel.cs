using AnalyticsServer.Models.Dto;
using AutoMapper;
using Redis.OM.Modeling;

namespace AnalyticsServer.Models;

[Document(StorageType = StorageType.Json, Prefixes = new[] { "UserModel" })]
public class UserModel
{
    [RedisIdField] [Indexed] public int Id { get; set; }
    [Indexed] public string FirstName { get; set; }
    [Indexed] public string LastName { get; set; }
    [Indexed] public string JobTitle { get; set; }
    [Indexed] public float BurnoutPercent { get; set; }
    [Indexed] public AnalyticsDataModel? AnalyticsData { get; set; }

    public AnalyticsDataModelDto GetAnalyticsData()
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<AnalyticsDataModel, AnalyticsDataModelDto>());
        var mapper = config.CreateMapper();
        var model = mapper.Map<AnalyticsDataModelDto>(AnalyticsData);
        model.Id = Id;
        return model;
    }
}


// List
// по id
// поиск