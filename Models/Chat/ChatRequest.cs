
using System.Text.Json.Serialization;

namespace LabPlatform.Models.Chat;

public class ChatRequest
{
    public List<string> History { get; set; }
    public string Question { get; set; }
}
