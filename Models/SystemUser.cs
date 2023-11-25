using System;
using System.Collections.Generic;

namespace LabPlatform.Models;

public partial class SystemUser
{
    public int Id { get; set; }

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool? RestartAccount { get; set; }

    public bool? ConfirmAccount { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int? BadConduct { get; set; }

    public string? Token { get; set; }

    public bool? Banned { get; set; }

    public byte[] Img { get; set; } = null!;

    public string? Club { get; set; }

    public string Rol { get; set; } = null!;

    public virtual ICollection<Article> ArticleAdminReviews { get; set; } = new List<Article>();

    public virtual ICollection<Article> ArticleSystemUsers { get; set; } = new List<Article>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Reaction> Reactions { get; set; } = new List<Reaction>();
}
