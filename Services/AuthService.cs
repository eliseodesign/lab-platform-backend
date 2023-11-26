using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LabPlatform.Models.DTOs;
using LabPlatform.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace LabPlatform;

public class AuthService : IAuthService
{
  private readonly IConfiguration _configuration; // para acceder a valores appsettings
  private readonly ISystemUserService _userService;

  public AuthService(IConfiguration configuration, ISystemUserService userService)
  {
    _configuration = configuration;
    _userService = userService;
  }

  private string GenerarToken(string idUsuario, string rolUser)
  {

    var key = _configuration.GetValue<string>("JwtSettings:key");
    var keyBytes = Encoding.ASCII.GetBytes(key);

    var claims = new ClaimsIdentity();
    claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, idUsuario));
    claims.AddClaim(new Claim(ClaimTypes.Role, rolUser));

    var credencialesToken = new SigningCredentials(
        new SymmetricSecurityKey(keyBytes),
        SecurityAlgorithms.HmacSha256Signature
    );

    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = claims,
      Expires = DateTime.UtcNow.AddDays(7),
      SigningCredentials = credencialesToken
    };

    var tokenHandler = new JwtSecurityTokenHandler();
    var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

    string tokenCreado = tokenHandler.WriteToken(tokenConfig);

    return tokenCreado;
  }
  public async Task<AuthResponse> GetToken(AuthRequest auth)
  {
    // validar cuenta 
    var exists = await _userService.GetByEmail(auth.Email);
    if (exists == null)
      return new AuthResponse() { Token = "", Result = false, Message = "No existe el usuario", User = null };

    // validar cuenta confirmada
    bool isVerifiedAccount = await _userService.ValidateConfirm(auth.Email);
    if (isVerifiedAccount == false)
      return new AuthResponse() { Token = "", Result = false, Message = "Debe confirmar su cuenta para iniciar sesión", User = null };

    // validar credenciales
    var userFound = await _userService.Validate(auth.Email, auth.Password);
    if (userFound == null)
      return new AuthResponse() { Token = "", Result = false, Message = "Contraseña Incorrecta", User = null };

    string tokenCreado = GenerarToken(userFound.Id.ToString(), userFound.Rol);

    //string refreshTokenCreado = GenerarRefreshToken();

    LoginResponse user = new LoginResponse()
    {
      Id = userFound.Id,
      FirstName = userFound.FirstName,
      LastName = userFound.LastName,
      BadConduct = userFound.BadConduct,
      Email = userFound.Email,
      Banned = userFound.Banned,
      Rol = userFound.Rol

    };

    return new AuthResponse() { Token = tokenCreado, Result = true, Message = "Ok", User = user };

    //return await GuardarHistorialRefreshToken(userFound.IdUsuario, tokenCreado, refreshTokenCreado);
  }
}
