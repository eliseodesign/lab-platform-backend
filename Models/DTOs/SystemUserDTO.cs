namespace LabPlatform.Models.DTOs;

public class SystemUserDTO
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public byte[] Img { get; set; } = null!;

    public string? Club { get; set; }

    public string Rol { get; set; } = null!;
}