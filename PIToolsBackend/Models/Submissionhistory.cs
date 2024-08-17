using System;
using System.Collections.Generic;

namespace PIToolsBackend.Models;

public partial class Submissionhistory
{
    public int? Userid { get; set; }

    public int? Scenarioid { get; set; }

    public int Submissionid { get; set; }

    public int? Newrank { get; set; }

    public double? Newpotential { get; set; }

    public int? Newtotalscore { get; set; }

    public DateTime? Submitdate { get; set; }

    public virtual Scenario? Scenario { get; set; }

    public virtual ICollection<Score> Scores { get; set; } = new List<Score>();

    public virtual Player? User { get; set; }
}
