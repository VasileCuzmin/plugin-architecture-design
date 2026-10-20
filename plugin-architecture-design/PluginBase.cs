// Abstract base class for all plugins
public abstract class PluginBase : IPlugin
{
    // Concrete properties with default implementations
    public virtual string Id { get; protected set; }
    public virtual string Name { get; protected set; }
    public virtual string Version { get; protected set; }
    public virtual string Description { get; protected set; }
    public virtual string Author { get; protected set; }
    public virtual IEnumerable<string> Dependencies { get; protected set; }
    public virtual IEnumerable<string> SupportedVersions { get; protected set; }

    public bool IsInitialized { get; private set; }

    protected IPluginContext Context { get; private set; }
    protected ILogger? Logger => Context?.Logger;

    protected PluginBase(string id, string name, string version, string description, string author)
    {
        Id = id;
        Name = name;
        Version = version;
        Description = description;
        Author = author;
        Dependencies = [];
        SupportedVersions = ["1.0"];
        IsInitialized = false;
    }

    // Concrete initialization with template method pattern
    //where is this called?
    // It is called externally by the plugin host to initialize the plugin with the provided context.
    // It ensures that the plugin is properly initialized before it can be started, stopped, or cleaned up.
    // The template method pattern ensures that common initialization logic is handled here,
    // while plugin-specific initialization is delegated to the abstract InitializePluginAsync() method.
    //This method should not be called from ourside
    // It should only be called by the plugin host, not by external consumers.

    // This method is the entry point for plugin initialization and should be called by the plugin host.
    // It handles common initialization logic and delegates plugin-specific initialization to InitializePluginAsync().
    // who is the plugin host?
    // The plugin host is the external system or application responsible for managing the lifecycle of plugins.
    // It is the entity that calls InitializeAsync(), StartAsync(), StopAsync(), and CleanupAsync() on the plugin.
    public virtual async Task<bool> InitializeAsync(IPluginContext context)
    {
        try
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            Logger?.LogInformation($"Initializing plugin {Name} v{Version}");

            // Call abstract method for plugin-specific initialization
            var result = await InitializePluginAsync();

            IsInitialized = result;
            Logger?.LogInformation($"Plugin {Name} initialization {(result ? "succeeded" : "failed")}");
            return result;
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, $"Plugin {Name} initialization failed");
            return false;
        }
    }

    // TODO: Add abstract InitializePluginAsync() method
    protected abstract Task<bool> InitializePluginAsync();
    protected abstract Task<bool> StartPluginAsync();
    // TODO: Add concrete StartAsync() method with template method pattern
    // TODO: Add abstract StartPluginAsync() method
    protected abstract Task StopPluginAsync();
    protected abstract Task CleanupPluginAsync();
    // TODO: Add concrete StopAsync() and CleanupAsync() methods
    // TODO: Add abstract StopPluginAsync() and CleanupPluginAsync() methods
    // TODO: Add concrete GetStatusAsync() method
    // TODO: Add abstract GetPluginStatusAsync() method
    protected abstract Task<PluginStatus> GetPluginStatusAsync();

    // Protected helper methods for derived classes
    protected void ValidateInitialized()
    {
        if (!IsInitialized)
            throw new InvalidOperationException($"Plugin {Name} is not initialized");
    }

    protected async Task<T> ExecuteWithErrorHandlingAsync<T>(Func<Task<T>> operation, T defaultValue = default)
    {
        try
        {
            return await operation();
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, $"Operation failed in plugin {Name}");
            return defaultValue;
        }
    }

    public Task<bool> StartAsync()
    {
        return ExecuteWithErrorHandlingAsync(async () =>
        {
            ValidateInitialized();
            Logger?.LogInformation($"Starting plugin {Name}");
            var result = await StartPluginAsync();
            Logger?.LogInformation($"Plugin {Name} start {(result ? "succeeded" : "failed")}");
            return result;
        }, false);
    }

    public Task StopAsync()
    {
        return ExecuteWithErrorHandlingAsync(async () =>
        {
            ValidateInitialized();
            Logger?.LogInformation($"Stopping plugin {Name}");
            await StopPluginAsync();
            Logger?.LogInformation($"Plugin {Name} stopped");
            return true;
        }, false);
    }

    public Task CleanupAsync()
    {
        return ExecuteWithErrorHandlingAsync(async () =>
        {
            ValidateInitialized();
            Logger?.LogInformation($"Cleaning up plugin {Name}");
            await CleanupPluginAsync();
            Logger?.LogInformation($"Plugin {Name} cleaned up");
            return true;
        }, false);
    }

    public Task<PluginStatus> GetStatusAsync()
    {
        return ExecuteWithErrorHandlingAsync(async () =>
        {
            ValidateInitialized();
            Logger?.LogInformation($"Getting status for plugin {Name}");
            var status = await GetPluginStatusAsync();
            Logger?.LogInformation($"Plugin {Name} status: {status}");
            return status;
        }, PluginStatus.Unknown);
    }
}
