using System;
using System.Collections.Generic;

namespace PIToolsBackend.Models;

public partial class Minigameuser
{
    public int Uid { get; set; }

    public string? Username { get; set; }

    public string? Nickname { get; set; }

    public string? Password { get; set; }

    public DateTime? Registrationdate { get; set; }
}
