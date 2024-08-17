using System;
using System.Collections.Generic;

namespace PIToolsBackend.Models;

public partial class Scenario
{
    public int Scenarioid { get; set; }

    public string? Scenarioname { get; set; }

    public double? Multiplier { get; set; }

    public double? Constant { get; set; }

    public string? Author { get; set; }

    public string? Filename { get; set; }

    public bool? Preload { get; set; }

    public string? Packid { get; set; }

    public DateOnly? Date { get; set; }

    public string? Pinyininname { get; set; }

    public bool? Available { get; set; }

    public int? Feature { get; set; }

    public virtual ICollection<Scenarioalias> Scenarioaliases { get; set; } = new List<Scenarioalias>();

    public virtual ICollection<Score> Scores { get; set; } = new List<Score>();

    public virtual ICollection<Submissionhistory> Submissionhistories { get; set; } = new List<Submissionhistory>();
}
