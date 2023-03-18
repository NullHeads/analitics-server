using Redis.OM.Modeling;

namespace AnalyticsServer.Models;

[Document(StorageType = StorageType.Json, Prefixes = new[] { "AnalyticsModel" })]
public class AnalyticsModel
{
    [RedisIdField] [Indexed] public int Id { get; set; }
    [Indexed] public string FirstName { get; set; }
    [Indexed] public string LastName { get; set; }
    [Indexed] public string JobTitle { get; set; }
    [Indexed] public float BurnoutPercent { get; set; }
}


// List
// по id
// поиск