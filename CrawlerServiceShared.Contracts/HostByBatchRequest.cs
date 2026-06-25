namespace CrawlerServiceShared.Contracts;

public sealed class HostByBatchRequest
{
    public string BatchName { get; set; } = string.Empty;
    public string SchemeName { get; set; } = string.Empty;
    public string HostName { get; set; } = string.Empty;
}
