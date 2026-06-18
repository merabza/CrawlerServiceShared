using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CrawlerServiceShared.Contracts.V1.Routes;
using LanguageExt;
using Microsoft.Extensions.Logging;
using OneOf;
using SystemTools.ReCounterContracts;
using SystemTools.SystemToolsShared.Errors;

namespace CrawlerServiceShared.Contracts;

public sealed class CrawlerServiceApiClient : ReCounterApiClient
{
    // ReSharper disable once ConvertToPrimaryConstructor
    public CrawlerServiceApiClient(ILogger logger, IHttpClientFactory httpClientFactory, string server, string? apiKey,
        bool useConsole) : base(logger, httpClientFactory, new ReCounterMessageHubClient(server, apiKey), server,
        apiKey, useConsole)
    {
    }

    public ValueTask<Option<Error[]>> RunBatch(string batchName, int newPartsCreateLimit,
        CancellationToken cancellationToken = default)
    {
        return PostAsync(
            CrawlerServiceApiRoutes.CrawlerRoute.CrawlerBase + CrawlerServiceApiRoutes.CrawlerRoute.RunBatch +
            "/?batchName=" + Uri.EscapeDataString(batchName) + "&newPartsCreateLimit=" + newPartsCreateLimit,
            cancellationToken);
    }

    public Task<OneOf<CrawlerPreCheckResult, Error[]>> PreCheck(string name, string? pageAddress,
        CancellationToken cancellationToken = default)
    {
        string afterServerAddress = CrawlerServiceApiRoutes.CrawlerRoute.CrawlerBase +
                                    CrawlerServiceApiRoutes.CrawlerRoute.PreCheck + "/?name=" +
                                    Uri.EscapeDataString(name) +
                                    (string.IsNullOrWhiteSpace(pageAddress)
                                        ? string.Empty
                                        : "&url=" + Uri.EscapeDataString(pageAddress));
        return GetAsyncReturn<CrawlerPreCheckResult>(afterServerAddress, false, cancellationToken);
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
