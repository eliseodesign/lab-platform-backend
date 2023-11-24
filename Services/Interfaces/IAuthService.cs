using LabPlatform.Models.DTOs;
namespace LabPlatform;

public interface IAuthService
{
  Task<AuthResponse> GetToken(AuthRequest auth);
}
