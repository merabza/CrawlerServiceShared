namespace CrawlerServiceShared.Contracts;

public sealed class TaskStartPointDto
{
    public int TspId { get; set; }
    public int TaskId { get; set; }
    public string StartPoint { get; set; } = string.Empty;
}
