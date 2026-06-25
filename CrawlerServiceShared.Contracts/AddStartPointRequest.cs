namespace CrawlerServiceShared.Contracts;

public sealed class AddStartPointRequest
{
    public int TaskId { get; set; }
    public string StartPoint { get; set; } = string.Empty;
}
