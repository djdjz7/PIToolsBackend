using System;
using System.Collections.Generic;

namespace PIToolsBackend.Models;

public partial class Minigamescore
{
    public int Id { get; set; }

    public int? Userid { get; set; }

    public DateTime? Submissiondate { get; set; }

    public int? Score { get; set; }
}
