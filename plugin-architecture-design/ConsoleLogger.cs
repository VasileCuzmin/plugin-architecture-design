internal class ConsoleLogger : ILogger
{
    public void LogInformation(string message)
    {
        Console.WriteLine($"INFO: {message}");
    }

    public void LogWarning(string message)
    {
        Console.WriteLine($"WARN: {message}");
    }

    public void LogError(string message)
    {
        Console.WriteLine($"ERROR: {message}");
    }

    public void LogError(Exception ex, string message)
    {
        Console.WriteLine($"ERROR: {message} - Exception: {ex.Message}");
    }

    public void LogError(Exception ex, string message, object context)
    {
        Console.WriteLine($"ERROR: {message} - Exception: {ex.Message} - Context: {context}");
    }
}