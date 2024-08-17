namespace PIToolsBackend.Models
{
    public class ScenarioResponse
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public double ScoreMultiplier { get; set; }
        public double Constant { get; set; }
        public string? Author { get; set; }
        public int Feature { get; set; }
        public string? Package { get; set; }
    }
}
