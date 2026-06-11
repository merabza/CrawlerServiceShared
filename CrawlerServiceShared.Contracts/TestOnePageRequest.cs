using System.Collections.Generic;

namespace CrawlerServiceShared.Contracts;

public sealed class TestOnePageRequest
{
    public string? TaskName { get; set; }
    public string? Url { get; set; }
    public List<string> StartPoints { get; set; } = [];
}
