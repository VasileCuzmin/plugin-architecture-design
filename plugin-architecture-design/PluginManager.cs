public class PluginManager : IPluginManager
{
    private readonly Dictionary<string, IPlugin> loadedPlugins;
    private readonly ILogger logger;
    private readonly IServiceProvider serviceProvider;

    public PluginManager(ILogger logger, IServiceProvider serviceProvider)
    {
        this.loadedPlugins = new Dictionary<string, IPlugin>();
        this.logger = logger;
        this.serviceProvider = serviceProvider;
    }

    public async Task<IEnumerable<IPlugin>> DiscoverPluginsAsync(string pluginPath)
    {
        logger.LogInformation($"Discovering plugins in: {pluginPath}");

        // TODO: In a real implementation, this would scan assemblies in pluginPath
        // For this demo, we'll return hard-coded plugin instances
        var discoveredPlugins = new List<IPlugin>
            {
                new TextContentProcessor(),
                new CloudStorageProvider(),
                new BasicAnalyticsEngine()
            };

        logger.LogInformation($"Discovered {discoveredPlugins.Count} plugins");
        return discoveredPlugins;
    }

    public async Task<bool> LoadPluginAsync(IPlugin plugin)
    {
        try
        {
            logger.LogInformation($"Loading plugin: {plugin.Name}");

            // Validate plugin before loading
            if (!await ValidatePluginAsync(plugin))
            {
                logger.LogError($"Plugin validation failed: {plugin.Name}");
                return false;
            }

            // Create plugin context
            var context = CreatePluginContext(plugin);

            // Initialize and start plugin
            if (await plugin.InitializeAsync(context) && await plugin.StartAsync())
            {
                loadedPlugins[plugin.Id] = plugin;
                logger.LogInformation($"Successfully loaded plugin: {plugin.Name}");
                return true;
            }

            logger.LogError($"Failed to initialize plugin: {plugin.Name}");
            return false;
        }
        catch (Exception ex)
        {
            logger.LogError($"Error loading plugin {plugin.Name}: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> UnloadPluginAsync(string pluginId)
    {
        if (!loadedPlugins.TryGetValue(pluginId, out var plugin))
        {
            logger.LogWarning($"Plugin not found for unloading: {pluginId}");
            return false;
        }

        try
        {
            logger.LogInformation($"Unloading plugin: {plugin.Name}");

            await plugin.StopAsync();
            await plugin.CleanupAsync();

            loadedPlugins.Remove(pluginId);
            logger.LogInformation($"Successfully unloaded plugin: {plugin.Name}");
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError($"Error unloading plugin {plugin.Name}: {ex.Message}");
            return false;
        }
    }

    public async Task<IPlugin> GetPluginAsync(string pluginId)
    {
        loadedPlugins.TryGetValue(pluginId, out var plugin);
        return plugin;
    }

    public async Task<IEnumerable<IPlugin>> GetPluginsByTypeAsync<T>() where T : class, IPlugin
    {
        return loadedPlugins.Values.OfType<T>().ToList();
    }

    public async Task<bool> ValidatePluginAsync(IPlugin plugin)
    {
        // TODO: Implement comprehensive plugin validation
        // - Check version compatibility
        // - Verify dependencies are available
        // - Validate plugin metadata
        // - Security checks

        return !string.IsNullOrEmpty(plugin.Id) &&
!string.IsNullOrEmpty(plugin.Name) &&
!loadedPlugins.ContainsKey(plugin.Id);
    }

    private IPluginContext CreatePluginContext(IPlugin plugin)
    {
        return new PluginContext
        {
            PluginDirectory = $"/plugins/{plugin.Id}",
            Logger = logger,
            Configuration = serviceProvider.GetService<IConfiguration>(),
            ServiceProvider = serviceProvider,
            SharedData = new Dictionary<string, object>()
        };
    }
}
// Plugin context implementation
public class PluginContext : IPluginContext
{
    public string PluginDirectory { get; init; }
    public ILogger Logger { get; init; }
    public IConfiguration Configuration { get; init; }
    public IServiceProvider ServiceProvider { get; init; }
    public IDictionary<string, object> SharedData { get; init; }
}

// Demonstration program
