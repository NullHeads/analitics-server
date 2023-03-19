namespace AnalyticsServer.Models.Dto;

public class AnalyticsResultDto
{
    public int Id { get; set; }
    public float BurnoutPercent { get; set; }
    public List<string> influence { get; set; }
}