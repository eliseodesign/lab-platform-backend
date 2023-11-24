using LabPlatform.Models;
namespace LabPlatform.Models.DTOs;

public class AuthResponse
{
    public required string Token { get; set; }
    public bool Result { get; set; }
    public required string Message { get; set; }
    public LoginResponse? User {get;set;}
}
