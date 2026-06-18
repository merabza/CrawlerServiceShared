namespace CrawlerServiceShared.Contracts;

public sealed class CrawlerPreCheckResult
{
    public bool AutoCreateNextPart { get; set; }
    public bool HasOpenPart { get; set; }
    public bool PageAlreadyAnalyzed { get; set; }
}
