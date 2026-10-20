public interface IPlugin : IPluginMetadata
{
    bool IsInitialized { get; }
    Task<bool> InitializeAsync(IPluginContext context);
    Task<bool> StartAsync();
    Task StopAsync();
    Task CleanupAsync();
    Task<PluginStatus> GetStatusAsync();
}

public enum PluginStatus
{
    NotLoaded, Loading, Loaded, Running, Stopping, Stopped, Error, Unknown
}