using System;
using System.Collections.Generic;

namespace PIToolsBackend.Models;

public partial class Player
{
    public int Userid { get; set; }

    public string? Qqnumber { get; set; }

    public string? Nickname { get; set; }

    public double? Potential { get; set; }

    public int? Totalscore { get; set; }

    public bool? Banned { get; set; }

    public long? Scoresum { get; set; }

    public int? Perfectcount { get; set; }

    public int? Greatcount { get; set; }

    public int? Goodcount { get; set; }

    public int? Completecount { get; set; }

    public long? Fromfull { get; set; }

    public int? Rank { get; set; }

    public virtual ICollection<Score> Scores { get; set; } = new List<Score>();

    public virtual ICollection<Submissionhistory> Submissionhistories { get; set; } = new List<Submissionhistory>();
}
