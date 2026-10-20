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
}

public class ContentItem
{
    public required string ContentType { get; set; }
    public required string Data { get; set; }
}