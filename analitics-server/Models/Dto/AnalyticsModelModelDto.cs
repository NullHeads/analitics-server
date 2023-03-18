namespace AnalyticsServer.Models.Dto;

public class AnalyticsModelModelDto
{
    public Guid Id;
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string JobTitle { get; set; }
    public float BurnoutPercent { get; set; }
}