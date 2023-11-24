using System;
using System.Collections.Generic;

namespace LabPlatform.Models;

public partial class Clientuser
{
    public int Id { get; set; }

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool? Restartaccount { get; set; }

    public bool? Confirmaccount { get; set; }

    public string? Token { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public int? Badconduct { get; set; }

    public bool? Banned { get; set; }

    public string? Typeuserid { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual Typeuser? Typeuser { get; set; }
}
