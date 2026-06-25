using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CrawlerServiceShared.Contracts.V1.Routes;
using LanguageExt;
using OneOf;
using SystemTools.SystemToolsShared.Errors;

namespace CrawlerServiceShared.Contracts;

public sealed partial class CrawlerServiceApiClient
{
    public Task<OneOf<List<BatchDto>, Error[]>> GetBatchesList(CancellationToken cancellationToken = default)
    {
        return GetAsyncReturn<List<BatchDto>>(
            CrawlerServiceApiRoutes.BatchRoute.BatchBase + CrawlerServiceApiRoutes.BatchRoute.List, false,
            cancellationToken);
    }

    public async Task<OneOf<BatchDto?, Error[]>> GetBatchByName(string batchName,
        CancellationToken cancellationToken = default)
    {
        OneOf<ApiNullableResult<BatchDto>, Error[]> result = await GetAsyncReturn<ApiNullableResult<BatchDto>>(
            CrawlerServiceApiRoutes.BatchRoute.BatchBase + CrawlerServiceApiRoutes.BatchRoute.GetByName + "/?name=" +
            Uri.EscapeDataString(batchName), false, cancellationToken);
        return result.Match<OneOf<BatchDto?, Error[]>>(wrapper => wrapper.Value, errors => errors);
    }

    public Task<OneOf<BatchDto, Error[]>> CreateBatch(BatchDto batch, CancellationToken cancellationToken = default)
    {
        return PostAsyncReturn<BatchDto>(
            CrawlerServiceApiRoutes.BatchRoute.BatchBase + CrawlerServiceApiRoutes.BatchRoute.Create, false,
            JsonSerializer.Serialize(batch), cancellationToken);
    }

    public Task<Option<Error[]>> UpdateBatch(BatchDto batch, CancellationToken cancellationToken = default)
    {
        return PutAsync(CrawlerServiceApiRoutes.BatchRoute.BatchBase + CrawlerServiceApiRoutes.BatchRoute.Update,
            JsonSerializer.Serialize(batch), cancellationToken);
    }

    public ValueTask<Option<Error[]>> DeleteBatch(string batchName, CancellationToken cancellationToken = default)
    {
        return DeleteAsync(
            CrawlerServiceApiRoutes.BatchRoute.BatchBase + CrawlerServiceApiRoutes.BatchRoute.Delete + "/?name=" +
            Uri.EscapeDataString(batchName), cancellationToken);
    }

    public Task<OneOf<List<string>, Error[]>> GetHostStartUrlNamesByBatch(string batchName,
        CancellationToken cancellationToken = default)
    {
        return GetAsyncReturn<List<string>>(
            CrawlerServiceApiRoutes.BatchRoute.BatchBase + CrawlerServiceApiRoutes.BatchRoute.HostByBatchList +
            "/?batchName=" + Uri.EscapeDataString(batchName), false, cancellationToken);
    }

    public ValueTask<Option<Error[]>> AddHostByBatch(HostByBatchRequest request,
        CancellationToken cancellationToken = default)
    {
        return PostAsync(
            CrawlerServiceApiRoutes.BatchRoute.BatchBase + CrawlerServiceApiRoutes.BatchRoute.HostByBatchAdd, false,
            JsonSerializer.Serialize(request), cancellationToken);
    }

    public ValueTask<Option<Error[]>> RemoveHostByBatch(string batchName, string schemeName, string hostName,
        CancellationToken cancellationToken = default)
    {
        return DeleteAsync(
            CrawlerServiceApiRoutes.BatchRoute.BatchBase + CrawlerServiceApiRoutes.BatchRoute.HostByBatchRemove +
            "/?batchName=" + Uri.EscapeDataString(batchName) + "&schemeName=" + Uri.EscapeDataString(schemeName) +
            "&hostName=" + Uri.EscapeDataString(hostName), cancellationToken);
    }
}
