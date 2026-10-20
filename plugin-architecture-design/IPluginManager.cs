public interface IPluginManager
{
    Task<IEnumerable<IPlugin>> DiscoverPluginsAsync(string pluginPath);
    Task<bool> LoadPluginAsync(IPlugin plugin);
    Task<bool> UnloadPluginAsync(string pluginId);
    Task<IPlugin> GetPluginAsync(string pluginId);
    Task<IEnumerable<IPlugin>> GetPluginsByTypeAsync<T>() where T : class, IPlugin;
    Task<bool> ValidatePluginAsync(IPlugin plugin);
}