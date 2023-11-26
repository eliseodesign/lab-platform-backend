namespace LabPlatform;

public class LoginResponse
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int? BadConduct { get; set; }

    public bool? Banned { get; set; }

    public string Rol { get; set; } = null!;
}
