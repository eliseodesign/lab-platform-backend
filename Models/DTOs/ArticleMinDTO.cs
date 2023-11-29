namespace LabPlatform.Models.DTOs;

public class ArticleMinDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string? Keywords { get; set; }
    public string? CreateAt { get; set; }
    public string ArticleType { get; set; } = null!;
    public SystemUserDTO SystemUser { get; set; } = null!;

}