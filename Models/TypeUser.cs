using System;
using System.Collections.Generic;

namespace LabPlatform.Models;

public partial class Typeuser
{
    public string Typename { get; set; } = null!;

    public virtual ICollection<Clientuser> Clientusers { get; set; } = new List<Clientuser>();
}
