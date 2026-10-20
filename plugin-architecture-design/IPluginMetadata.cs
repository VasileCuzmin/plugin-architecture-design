public interface IPluginMetadata
{
    string Id { get; }
    string Name { get; }
    string Version { get; }
    string Description { get; }
    string Author { get; }
    IEnumerable<string> Dependencies { get; }
    IEnumerable<string> SupportedVersions { get; }
}
