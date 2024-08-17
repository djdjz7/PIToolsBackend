using System;
using System.Collections.Generic;

namespace PIToolsBackend.Models;

public partial class Score
{
    public int Scenarioid { get; set; }

    public int Userid { get; set; }

    public DateTime? Achievedate { get; set; }

    public int? Score1 { get; set; }

    public double? Rating { get; set; }

    public int? Submissionid { get; set; }

    public virtual Scenario Scenario { get; set; } = null!;

    public virtual Submissionhistory? Submission { get; set; }

    public virtual Player User { get; set; } = null!;
}
