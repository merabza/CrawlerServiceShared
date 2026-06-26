using System.Collections.Generic;
using SystemTools.SystemToolsShared;

namespace CrawlerServiceShared.Contracts;

public sealed class TaskDto : ItemData
{
    public int TaskId { get; set; }
    public string TaskName { get; set; } = string.Empty;
    public List<TaskStartPointDto> StartPoints { get; set; } = [];
}
