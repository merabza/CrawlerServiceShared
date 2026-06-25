using SystemTools.SystemToolsShared;

namespace CrawlerServiceShared.Contracts;

public sealed class HostDto : ItemData
{
    public int HostId { get; set; }
    public string HostName { get; set; } = string.Empty;
    public bool HostProhibited { get; set; }
}
