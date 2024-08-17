using System;
using System.Collections.Generic;

namespace PIToolsBackend.Models;

public partial class Minigamelogin
{
    public int Id { get; set; }

    public int? Uid { get; set; }

    public DateTime? Logindate { get; set; }
}
