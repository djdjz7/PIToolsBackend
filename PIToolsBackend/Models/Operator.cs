using System;
using System.Collections.Generic;

namespace PIToolsBackend.Models;

public partial class Operator
{
    public int Opid { get; set; }

    public string Opqq { get; set; } = null!;

    public bool? Isadmin { get; set; }
}
