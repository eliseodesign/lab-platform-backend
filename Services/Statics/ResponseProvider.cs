namespace LabPlatform.Services.Statics;

public static class Res
{
  public static IReturn<T> Provider<T>(T data, string message, bool success)
  {
    return new Return<T> { Data = data, Message = message, Success = success };
  }
}

public interface IReturn<T>
{
  T Data { get; set; }
  string Message { get; set; }
  bool Success { get; set; }
}

public class Return<T> : IReturn<T>
{
  public required T Data { get; set; }
  public required string Message { get; set; }
  public bool Success { get; set; }
}