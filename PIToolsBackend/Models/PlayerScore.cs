namespace PIToolsBackend.Models
{
    public class PlayerScore
    {
        public PlayerCondensed Player { get; set; }
        public PlayerScenarioScore[] ScenarioScores { get; set; }
    }

    public class PlayerScenarioScore
    {
        public int ScenarioID { get; set; }
        public string? ScenarioName { get; set; }
        public int Score { get; set; }
        public double Rating { get; set; }
    }

    public class PlayerCondensed
    {
        public int UserID { get; set; }
        public string? QQID { get; set; }
        public string? Nickname { get; set; }
        public double Potential { get; set; }
        public bool Banned { get; set; }
        public long ScoreSum { get; set; }
        public int Rank { get; set; }
    }
}
