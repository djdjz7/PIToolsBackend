namespace PIToolsBackend.Models
{
    public class ScenarioScoreListResponse
    {
        required public ScenarioResponse Scenario { get; set; }
        required public ScenarioPlayerScore[] Players { get; set; }
    }

    public class ScenarioPlayerScore
    {
        required public string Nickname { get; set; }
        required public int Score { get; set; }
        required public int UserId { get; set; }
        required public string QqId { get; set; }
        required public double Potential { get; set; }
    }
}
