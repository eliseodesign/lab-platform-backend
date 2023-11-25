using System;
using System.Collections.Generic;

namespace LabPlatform.Models;

public partial class Notification
{
    public int Id { get; set; }

    public DateTime? CreateAt { get; set; }

    public string? Type { get; set; }

    public int? ToUser { get; set; }

    public int? SystemUserId { get; set; }

    public int? ArticleId { get; set; }

    public bool? IsRead { get; set; }

    public virtual SystemUser? SystemUser { get; set; }
}
