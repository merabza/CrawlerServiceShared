using SystemTools.SharedKernel;

namespace CrawlerServiceShared.Contracts.Errors;

public static class CrawlerServiceErrors
{
    public static Error CrawlAlreadyRunning =>
        Error.Conflict(nameof(CrawlAlreadyRunning), "Crawl process is already running");

    public static Error ParseOnePageParametersNotCreated =>
        Error.Problem(nameof(ParseOnePageParametersNotCreated), "ParseOnePageParameters does not created");

    public static Error BatchWithNameNotFound(string batchName)
    {
        return Error.NotFound(nameof(BatchWithNameNotFound), $"Batch with name {batchName} not found");
    }

    public static Error TaskWithNameNotFound(string? taskName)
    {
        return Error.NotFound(nameof(TaskWithNameNotFound), $"Task with name {taskName} not found");
    }

    public static Error HostWithNameNotFound(string? hostName)
    {
        return Error.NotFound(nameof(HostWithNameNotFound), $"Host with name {hostName} not found");
    }

    public static Error SchemeWithNameNotFound(string? schemeName)
    {
        return Error.NotFound(nameof(SchemeWithNameNotFound), $"Scheme with name {schemeName} not found");
    }

    public static Error StartPointNotFound(int taskId, string? startPoint)
    {
        return Error.NotFound(nameof(StartPointNotFound), $"Start point {startPoint} for task id {taskId} not found");
    }
}
