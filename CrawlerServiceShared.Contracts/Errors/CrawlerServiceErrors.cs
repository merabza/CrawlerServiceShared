using SystemTools.SystemToolsShared.Errors;

namespace CrawlerServiceShared.Contracts.Errors;

public static class CrawlerServiceErrors
{
    public static readonly ErrorOmd CrawlAlreadyRunning = new()
    {
        Code = nameof(CrawlAlreadyRunning), Name = "Crawl process is already running"
    };

    public static readonly ErrorOmd ParseOnePageParametersNotCreated = new()
    {
        Code = nameof(ParseOnePageParametersNotCreated), Name = "ParseOnePageParameters does not created"
    };

    public static ErrorOmd BatchWithNameNotFound(string batchName)
    {
        return new ErrorOmd { Code = nameof(BatchWithNameNotFound), Name = $"Batch with name {batchName} not found" };
    }

    public static ErrorOmd TaskWithNameNotFound(string? taskName)
    {
        return new ErrorOmd { Code = nameof(TaskWithNameNotFound), Name = $"Task with name {taskName} not found" };
    }

    public static ErrorOmd HostWithNameNotFound(string? hostName)
    {
        return new ErrorOmd { Code = nameof(HostWithNameNotFound), Name = $"Host with name {hostName} not found" };
    }

    public static ErrorOmd SchemeWithNameNotFound(string? schemeName)
    {
        return new ErrorOmd
        {
            Code = nameof(SchemeWithNameNotFound), Name = $"Scheme with name {schemeName} not found"
        };
    }

    public static ErrorOmd StartPointNotFound(int taskId, string? startPoint)
    {
        return new ErrorOmd
        {
            Code = nameof(StartPointNotFound), Name = $"Start point {startPoint} for task id {taskId} not found"
        };
    }
}
