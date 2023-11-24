using System.Reflection;
using LabPlatform.Models;
using LabPlatform.Models.DTOs;
using LabPlatform.Schemas;
using LabPlatform.Services.Interfaces;
using LabPlatform.Services.Statics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabPlatform;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
  private readonly IWebHostEnvironment _webHostEnvironment;
  private readonly IClienteUserService _userService;
  private readonly IEmailService _emailService;
  private readonly IAuthService _authService;
  public AuthController(IClienteUserService userService, IWebHostEnvironment webHostEnvironment, IEmailService emailService, IAuthService authService)
  {
    _userService = userService;
    _webHostEnvironment = webHostEnvironment;
    _emailService = emailService;
    _authService = authService;
  }


  [HttpPost]
  [Route("login")]
  public async Task<IActionResult> Login([FromBody] AuthRequest auth)
  {
    string? validCredentials = UtilsService.ValidCredentials(auth.Email, auth.Password);
    if (validCredentials != null)
    {
      return BadRequest(Res.Provider(new { }, validCredentials, false));
    }
    auth.Password = UtilsService.ConvertSHA256(auth.Password);
    var result = await _authService.GetToken(auth);
    if (result.Result == false)
    {
      return Unauthorized(Res.Provider(new { }, result.Message, false));
    }

    return Ok(Res.Provider(result, "Usuario encontrado", true));
  }

  // [Authorize]
  [HttpPost]
  [Route("ping")]
  public async Task<IActionResult> Ping()
  {
    return Ok(Res.Provider(new { }, "Pong", true));
  }
  [HttpPost]
  [Route("register")]
  public async Task<IActionResult> Reg([FromBody] CreateClientUser usuario)
  {
    string? validCredentials = UtilsService.ValidCredentials(usuario.Email, usuario.Password);
    if (validCredentials != null)
    {
      return BadRequest(Res.Provider(new { }, validCredentials, false));
    }

    if (await _userService.GetByEmail(usuario.Email) == null)
    {
      string typeUser = UtilsService.ValidEsfeEmail(usuario.Email) ? "esfe-user" : "no-esfe-user";

      Clientuser nuevo = new Clientuser()
      {
        Email = usuario.Email,
        Password = UtilsService.ConvertSHA256(usuario.Password),
        Firstname = usuario.FirstName,
        Lastname = usuario.LastName,
        Restartaccount = false,
        Confirmaccount = false,
        Token = UtilsService.RandomCode(),
        Typeuserid = typeUser

      };

      bool respuesta = await _userService.Create(nuevo);

      if (respuesta)
      {
        // TODO: FIX
        // string content = this.GetFileContent("Templates/VerifyEmail.html");
        string content = this.GetFileContent();

        content = content.Replace("{{FirstName}}", usuario.FirstName);
        content = content.Replace("{{LastName}}", usuario.LastName);
        content = content.Replace("{{Token}}", nuevo.Token);

        // string htmlBody = string.Format(content, $"{usuario.FirstName} {usuario.LastName}", nuevo.Token);

        EmailDTO EmailDTO = new EmailDTO()
        {
          To = usuario.Email,
          Subject = "Correo confirmación",
          Content = content
        };

        bool enviado = _emailService.SendEmail(EmailDTO);
        if (enviado == true)
        {
          return Ok(Res.Provider(new { }, $"Su cuenta ha sido creada. Hemos enviado un mensaje al correo {usuario.Email} para confirmar su cuenta", true));
        }
        else
        {
          return BadRequest(Res.Provider(new { }, "Su cuenta no se pudo crear", true));
        }
      }
      else
      {
        return BadRequest(Res.Provider(new { }, "No se pudo crear su cuenta", false));
      }
    }
    else
    {
      return BadRequest(Res.Provider(new { }, $"Ya existe un usuario registrado con {usuario.Email}", false));
    }
  }

  [HttpGet]
  [Route("verify")]
  public async Task<IActionResult> Confirm(string token)
  {
    bool result = await _userService.ConfirmAccount(token);
    if (result == true)
    {
      return Ok(Res.Provider(new { }, "Cuenta confirmada", true));
    }
    else
    {
      return BadRequest(Res.Provider(new { }, "Error al confirmar cuenta", false));
    }
  }


  // private string GetFileContent(string filePath)
  // {
  //   string basePath = _webHostEnvironment.ContentRootPath;
  //   string fullPath = Path.Combine(basePath, filePath);

  //   if (System.IO.File.Exists(fullPath))
  //   {
  //     return System.IO.File.ReadAllText(fullPath);
  //   }

  //   throw new FileNotFoundException("El archivo no existe", fullPath);
  // }

  // TODO!: METODO TEMPORAL -> FIX : problema en producción docker no encuentra el archivo
  // probabablemente por la estructura de carpetas o por los permisos
  private string GetFileContent()
  {
    return @"
       <!DOCTYPE html>
<html lang='es'>

<head>
    <style>
        body {
            font-family: Sans-serif;
        }

        .container {
            width: 600px;
            padding: 20px;
            border: 1px solid #DBDBDB;
            border-radius: 12px;
        }

        .header {
            color: #6199c7;
            display: flex;
            align-items: center;
        }
        .header h1{
            font-family: 3em;
        }

        .header img {
            width: 50px;
            height: 50px;
            margin-right: 10px;
        }
        p{
            font-size: 18px;
        }
    </style>
</head>

<body>
    <div class='container'>
        <h1 class='header'>
            <img src='https://i.ibb.co/dQTK7wR/logo128.png' /> Hola {{FirstName}}, soy JulioGPT
        </h1>
        <p> Te escribo para verificar tu cuenta. <br>
        El codigo es: <b style='font-size: large; font-weight: bold;'>{{Token}}</b></p>
    </div>
</body>

</html>
       ";
  }
}
