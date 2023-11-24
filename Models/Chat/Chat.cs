
using System.Text.Json.Serialization;

namespace LabPlatform.Models.Chat;
public class Chat
{
    public string text { get; set; }
    public List<SourceDocument> sourceDocuments { get; set; }
}

public class SourceDocument
{
    public string pageContent { get; set; }
    public Metadata metadata { get; set; }
}

public class Metadata
{
    [JsonPropertyName("loc.lines.from")]
    public int loclinesfrom { get; set; }

    [JsonPropertyName("loc.lines.to")]
    public int loclinesto { get; set; }
    public string source { get; set; }
}