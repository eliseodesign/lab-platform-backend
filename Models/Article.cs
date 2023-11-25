using System;
using System.Collections.Generic;

namespace LabPlatform.Models;

public partial class Article
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string? Description { get; set; }

    public string? Keywords { get; set; }

    public DateTime? CreateAt { get; set; }

    public string ArticleState { get; set; } = null!;

    public string ArticleType { get; set; } = null!;

    public int SystemUserId { get; set; }

    public int? AdminReviewId { get; set; }

    public virtual SystemUser? AdminReview { get; set; }

    public virtual ICollection<Reaction> Reactions { get; set; } = new List<Reaction>();

    public virtual SystemUser SystemUser { get; set; } = null!;
}
