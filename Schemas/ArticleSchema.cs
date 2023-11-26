namespace LabPlatform.Schemas;
public class CreateArticle
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string? Description { get; set; }
    public string CreateAt { get; set; }
    public string? Keywords { get; set; }
    public string ArticleState { get; set; } = null!;
    public string ArticleType { get; set; } = null!;
    public int SystemUserId { get; set; }
}