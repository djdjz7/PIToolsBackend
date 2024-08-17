using System;
using System.Collections.Generic;

namespace PIToolsBackend.Models;

public partial class Scenarioalias
{
    public int Aliasid { get; set; }

    public int Scenarioid { get; set; }

    public string Alias { get; set; } = null!;

    public virtual Scenario Scenario { get; set; } = null!;
}
