public interface IContentProcessor : IPlugin
{
    IEnumerable<string> SupportedContentTypes { get; }
    Task<bool> CanProcessAsync(ContentItem content);
    Task<ProcessingResult> ProcessContentAsync(ContentItem content, ProcessingOptions options);
    Task<ValidationResult> ValidateContentAsync(ContentItem content);
}

public class ValidationResult
{
    public bool IsValid { get; set; }
    public string Message { get; set; } = string.Empty;
}

public class ProcessingOptions
{
    public bool ValidateOnly { get; set; } = false;
}

public class ProcessingResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string ProcessedContent { get; set; } = string.Empty;
    public int ProcessingTimeMs { get; set; }
    public Dictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();
}

public class ContentItem
{
    public required string Id { get; set; }
    public required string Content { get; set; }
    public required string ContentType { get; set; }
    public required byte[] Data { get; set; }
    public required Dictionary<string, object> Metadata { get;  set; }
}