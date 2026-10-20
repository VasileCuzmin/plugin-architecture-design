// Abstract base class for all plugins
public abstract class ContentProcessorBase : PluginBase, IContentProcessor
{
    public abstract IEnumerable<string> SupportedContentTypes { get; }

    protected ContentProcessorBase(string id, string name, string version, string description, string author)
    : base(id, name, version, description, author)
    {
    }

    public virtual async Task<bool> CanProcessAsync(ContentItem content)
    {
        ValidateInitialized();
        return SupportedContentTypes.Contains(content.ContentType);
    }

    public async Task<ProcessingResult> ProcessContentAsync(ContentItem content, ProcessingOptions options)
    {
        ValidateInitialized();
        return await ProcessContentImplementationAsync(content, options);
    }

    protected abstract Task<ProcessingResult> ProcessContentImplementationAsync(ContentItem content, ProcessingOptions options);

    // TODO: Add abstract ProcessContentAsync method
    protected abstract Task<ValidationResult> ValidateContentImplementationAsync(ContentItem content);

    // TODO: Add concrete ValidateContentAsync method with template method pattern
    public async Task<ValidationResult> ValidateContentAsync(ContentItem content)
    {
        ValidateInitialized();
        return await ValidateContentImplementationAsync(content);
    }
}