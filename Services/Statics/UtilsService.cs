using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace LabPlatform.Services.Statics;

public class UtilsService
{
  private static Random random = new Random();

  public static string ConvertSHA256(string text)
  {
    string hash = string.Empty;
    using (SHA256 sha256 = SHA256.Create())
    {
      //obtener el hash recibido
      byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));

      // convertir array de bytes en cadena de texto
      foreach (byte b in hashValue)
      {
        hash += $"{b:X2}";
      }
    }
    return hash;
  }

  public static string RandomCode()
  {
    const int length = 6;
    const string chars = "0123456789";
    char[] code = new char[length];

    for (int i = 0; i < length; i++)
    {
      code[i] = chars[random.Next(chars.Length)];
    }

    return new string(code);
  }

  public static bool ValidEsfeEmail(string email)
  {
    // Expresión regular validar formato del correo
    string patron = @"^[a-zA-Z0-9._%+-]+@esfe\.agape\.edu\.sv$";
    return Regex.IsMatch(email, patron);
  }

  public static string? ValidCredentials(string email, string password)
  {
    // Expresión regular para validar un correo electrónico
    string emailPattern = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$";

    if (!Regex.IsMatch(email, emailPattern))
    {
      return "El correo electrónico no es válido.";
    }

    // Validar la contraseña
    if (string.IsNullOrEmpty(password) || password.Length < 8)
    {
      return "La contraseña debe tener al menos 8 caracteres.";
    }

    // Si todos los criterios pasan, devuelve null para indicar que todo es correcto.
    return null;
  }

  public static async Task BadRequest(HttpContext context, string error)
  {
    Console.WriteLine("BadRequest MIDDLEWARE");
    context.Response.StatusCode = 200; // Código de respuesta prohibido (puedes usar otro código según tus necesidades)
    context.Response.ContentType = "application/json";

    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
    context.Response.Headers.Add("Access-Control-Allow-Headers", "*");
    context.Response.Headers.Add("Access-Control-Allow-Methods", "*");

    var data = Res.Provider(new { }, error, false);
    await context.Response.WriteAsJsonAsync(data);
    return;
  }
}
