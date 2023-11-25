using System;
using System.Collections.Generic;

namespace LabPlatform.Models;

public partial class Reaction
{
    public int Id { get; set; }

    public DateTime? CreateAt { get; set; }

    public string Content { get; set; } = null!;

    public int SystemUserId { get; set; }

    public int ArticleId { get; set; }

    public int? ParentId { get; set; }

    public virtual Article Article { get; set; } = null!;

    public virtual SystemUser SystemUser { get; set; } = null!;
}
