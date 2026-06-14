using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CrawlerServiceShared.Contracts.V1.Routes;
using LanguageExt;
using Microsoft.Extensions.Logging;
using SystemTools.ReCounterContracts;
using SystemTools.SystemToolsShared.Errors;

namespace CrawlerServiceShared.Contracts;

public sealed class CrawlerServiceApiClient : ReCounterApiClient
{
    public CrawlerServiceApiClient(ILogger logger, IHttpClientFactory httpClientFactory, string server, string? apiKey,
        bool useConsole) : base(logger, httpClientFactory, new ReCounterMessageHubClient(server, apiKey), server,
        apiKey, useConsole)
    {
    }

    public ValueTask<Option<Error[]>> RunBatch(string batchName, CancellationToken cancellationToken = default)
    {
        return PostAsync(
            CrawlerServiceApiRoutes.CrawlerRoute.CrawlerBase + CrawlerServiceApiRoutes.CrawlerRoute.RunBatch +
            "/?batchName=" + Uri.EscapeDataString(batchName), cancellationToken);
    }

    public ValueTask<Option<Error[]>> RunTask(RunTaskRequest request, CancellationToken cancellationToken = default)
    {
        return PostAsync(
            CrawlerServiceApiRoutes.CrawlerRoute.CrawlerBase + CrawlerServiceApiRoutes.CrawlerRoute.RunTask, true,
            JsonSerializer.Serialize(request), cancellationToken);
    }

    public ValueTask<Option<Error[]>> TestOnePage(TestOnePageRequest request,
        CancellationToken cancellationToken = default)
    {
        return PostAsync(
            CrawlerServiceApiRoutes.CrawlerRoute.CrawlerBase + CrawlerServiceApiRoutes.CrawlerRoute.TestOnePage, true,
            JsonSerializer.Serialize(request), cancellationToken);
    }
}
