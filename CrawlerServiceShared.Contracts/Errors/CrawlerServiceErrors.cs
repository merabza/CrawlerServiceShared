using SystemTools.SystemToolsShared.Errors;

namespace CrawlerServiceShared.Contracts.Errors;

public static class CrawlerServiceErrors
{
    public static readonly Error CrawlAlreadyRunning = new()
    {
        Code = nameof(CrawlAlreadyRunning), Name = "Crawl process is already running"
    };

    public static readonly Error ParseOnePageParametersNotCreated = new()
    {
        Code = nameof(ParseOnePageParametersNotCreated), Name = "ParseOnePageParameters does not created"
    };

    public static Error BatchWithNameNotFound(string batchName)
    {
        return new Error { Code = nameof(BatchWithNameNotFound), Name = $"Batch with name {batchName} not found" };
    }

    public static Error TaskWithNameNotFound(string? taskName)
    {
        return new Error { Code = nameof(TaskWithNameNotFound), Name = $"Task with name {taskName} not found" };
    }
}
