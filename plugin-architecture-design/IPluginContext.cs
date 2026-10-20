public interface IPluginContext
{
    string PluginDirectory { get; }
    ILogger Logger { get; }
    IConfiguration Configuration { get; }
    IServiceProvider ServiceProvider { get; }
    IDictionary<string, object> SharedData { get; }
}
