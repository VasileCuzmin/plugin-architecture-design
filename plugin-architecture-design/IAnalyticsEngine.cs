public interface IAnalyticsEngine : IPlugin
{
    IEnumerable<string> SupportedMetrics { get; }
    Task<AnalyticsReport> GenerateReportAsync(AnalyticsQuery query);
    Task RecordEventAsync(AnalyticsEvent analyticsEvent);
    Task<IEnumerable<AnalyticsInsight>> GetInsightsAsync(string contentId);
}

public class AnalyticsReport
{
    public string Title { get; set; } = string.Empty;
    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
    public IDictionary<string, object> Metrics { get; set; } = new Dictionary<string, object>();
}

public class AnalyticsQuery
{
    public string ContentId { get; set; } = string.Empty;
    public DateTime StartDate { get; set; } = DateTime.UtcNow.AddDays(-7);
    public DateTime EndDate { get; set; } = DateTime.UtcNow;
}

public class AnalyticsEvent
{
    public string ContentId { get; set; } = string.Empty;
    public string EventType { get; set; } = string.Empty;
    public DateTime OccurredAt { get; set; } = DateTime.UtcNow;
}

public class AnalyticsInsight
{
    public string ContentId { get; set; } = string.Empty;
    public string InsightType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
}