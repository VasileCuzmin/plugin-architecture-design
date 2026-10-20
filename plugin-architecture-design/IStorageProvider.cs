public interface IStorageProvider : IPlugin
{
    StorageCapabilities Capabilities { get; }
    Task<string> StoreContentAsync(ContentItem content, StorageOptions options);
    Task<ContentItem> RetrieveContentAsync(string contentId, RetrievalOptions options);
    Task<bool> DeleteContentAsync(string contentId);
    Task<StorageStatistics> GetStatisticsAsync();
}

public class StorageOptions
{
    public bool OverwriteExisting { get; set; } = false;
}

public class RetrievalOptions
{
    public bool IncludeMetadata { get; set; } = false;
}

public class StorageStatistics
{
    public long TotalContentItems { get; set; } = 0;
    public long TotalStorageSize { get; set; } = 0;
}

public class StorageCapabilities
{
    public bool CanStoreContent { get; set; } = false;
    public bool CanRetrieveContent { get; set; } = false;
    public bool CanDeleteContent { get; set; } = false;
    public bool CanGetStatistics { get; set; } = false;
}