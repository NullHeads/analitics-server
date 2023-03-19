namespace AnalyticsServer.Models.Dto;

public class UserModelDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string JobTitle { get; set; }
    public float BurnoutPercent { get; set; }
}