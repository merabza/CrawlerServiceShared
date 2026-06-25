using SystemTools.SystemToolsShared;

namespace CrawlerServiceShared.Contracts;

public sealed class BatchDto : ItemData
{
    public int BatchId { get; set; }
    public string BatchName { get; set; } = string.Empty;
    public bool IsOpen { get; set; }
    public bool AutoCreateNextPart { get; set; }
}
