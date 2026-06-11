using System.Collections.Generic;

namespace CrawlerServiceShared.Contracts;

public sealed class RunTaskRequest
{
    public string? TaskName { get; set; }
    public List<string> StartPoints { get; set; } = [];
}
