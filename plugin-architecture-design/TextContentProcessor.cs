public class TextContentProcessor : ContentProcessorBase
{

    public TextContentProcessor()
    : base("text-processor", "Text Content Processor", "1.0.0",
    "Processes text-based content with various formatting options", "TechFlow Corp")
    {
    }
    
    public override IEnumerable<string> SupportedContentTypes => ["text/plain", "text/markdown", "text/html"];

    protected override Task CleanupPluginAsync()
    {
        Logger.LogInformation("Cleaning up text content processor resources");
        // TODO: Cleanup any resources, stop background services, etc.
        return Task.CompletedTask;
    }

    protected override Task<PluginStatus> GetPluginStatusAsync()
    {
        Logger.LogInformation("Getting plugin status");
        // TODO: Implement actual status retrieval logic
        return Task.FromResult(PluginStatus.Running);
    }

    protected async override Task<bool> InitializePluginAsync()
    {
        Logger.LogInformation("Initializing text processing capabilities");
        // TODO: Initialize text processing libraries, load dictionaries, etc.
        await Task.Delay(100); // Simulate initialization work
        return true;
    }

    protected async override Task<bool> StartPluginAsync()
    {
        Logger.LogInformation("Starting text content processor");
        // TODO: Start background services, warm up caches, etc.
        await Task.Delay(50);
        return true;
    }

    protected override Task StopPluginAsync()
    {
        Logger.LogInformation("Stopping text content processor");
        // TODO: Stop background services, flush caches, etc.
        return Task.CompletedTask;
    }

    protected override Task<ValidationResult> ValidateContentImplementationAsync(ContentItem content)
    {
        Logger.LogInformation("Validating content implementation");
        // TODO: Implement actual content validation logic
        return Task.FromResult(new ValidationResult { IsValid = true });
    }

    protected override async Task<ProcessingResult> ProcessContentImplementationAsync(ContentItem content, ProcessingOptions options)
    {
        Logger.LogInformation("Processing content of type: " + content.ContentType);
        // TODO: Implement actual content processing logic
        return await ExecuteWithErrorHandlingAsync(async () =>
   {
       Logger.LogInformation($"Processing text content: {content.Id}");

       // TODO: Implement text processing logic
       // TODO: Handle different text formats based on content type
       // TODO: Apply processing options (formatting, validation, etc.)

       var processedContent = content.Content.ToUpper(); // Simple example

       return new ProcessingResult
       {
           Success = true,
           ProcessedContent = processedContent,
           ProcessingTimeMs = 42,
           Metadata = new Dictionary<string, object>
           {
               ["OriginalLength"] = content.Content.Length,
               ["ProcessedLength"] = processedContent.Length,
               ["ProcessingType"] = "TextFormatting"
           }
       };
   }, new ProcessingResult { Success = false, Message = "Processing failed" });
    }
}