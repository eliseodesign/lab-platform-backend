namespace LabPlatform.Models.DTOs;

public class EmailDTO
{
  public required string To { get; set; }
  public required string Subject { get; set; }
  public required string Content { get; set; }
}
