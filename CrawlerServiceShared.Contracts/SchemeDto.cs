using SystemTools.SystemToolsShared;

namespace CrawlerServiceShared.Contracts;

public sealed class SchemeDto : ItemData
{
    public int SchId { get; set; }
    public string SchName { get; set; } = string.Empty;
    public bool SchProhibited { get; set; }
}
