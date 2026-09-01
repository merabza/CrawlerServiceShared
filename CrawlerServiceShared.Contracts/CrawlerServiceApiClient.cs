using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CrawlerServiceShared.Contracts.V1.Routes;
using Microsoft.Extensions.Logging;
using SystemTools.ReCounterContracts;
using SystemTools.SharedKernel;

namespace CrawlerServiceShared.Contracts;

public sealed class CrawlerServiceApiClient : ReCounterApiClient
{
    // ReSharper disable once ConvertToPrimaryConstructor
    public CrawlerServiceApiClient(ILogger logger, IHttpClientFactory httpClientFactory, string server, string? apiKey,
        bool useConsole) : base(logger, httpClientFactory, new ReCounterMessageHubClient(server, apiKey), server,
        apiKey, useConsole)
    {
    }

    public ValueTask<Result> RunBatch(string batchName, int newPartsCreateLimit, int progressDelaySeconds,
        CancellationToken cancellationToken = default)
    {
        return PostAsync(
            CrawlerServiceApiRoutes.CrawlerRoute.CrawlerBase + CrawlerServiceApiRoutes.CrawlerRoute.RunBatch +
            "/?batchName=" + Uri.EscapeDataString(batchName) + "&newPartsCreateLimit=" + newPartsCreateLimit +
            "&progressDelaySeconds=" + progressDelaySeconds, cancellationToken);
    }

    public Task<Result<CrawlerPreCheckResult>> PreCheck(string name, string? pageAddress,
        CancellationToken cancellationToken = default)
    {
        string afterServerAddress = CrawlerServiceApiRoutes.CrawlerRoute.CrawlerBase +
                                    CrawlerServiceApiRoutes.CrawlerRoute.PreCheck + "/?batchName=" +
                                    Uri.EscapeDataString(name) + (string.IsNullOrWhiteSpace(pageAddress)
                                        ? string.Empty
                                        : "&url=" + Uri.EscapeDataString(pageAddress));
        return GetAsyncReturn<CrawlerPreCheckResult>(afterServerAddress, false, cancellationToken);
    }

    public ValueTask<Result> RunTask(RunTaskRequest request, CancellationToken cancellationToken = default)
    {
        return PostAsync(
            CrawlerServiceApiRoutes.CrawlerRoute.CrawlerBase + CrawlerServiceApiRoutes.CrawlerRoute.RunTask, true,
            JsonSerializer.Serialize(request), cancellationToken);
    }

    public ValueTask<Result> TestOnePage(TestOnePageRequest request,
        CancellationToken cancellationToken = default)
    {
        return PostAsync(
            CrawlerServiceApiRoutes.CrawlerRoute.CrawlerBase + CrawlerServiceApiRoutes.CrawlerRoute.TestOnePage, true,
            JsonSerializer.Serialize(request), cancellationToken);
    }

    public Task<Result<List<BatchDto>>> GetBatchesList(CancellationToken cancellationToken = default)
    {
        return GetAsyncReturn<List<BatchDto>>(
            CrawlerServiceApiRoutes.BatchRoute.BatchBase + CrawlerServiceApiRoutes.BatchRoute.List, false,
            cancellationToken);
    }

    public Task<Result<BatchDto>> GetBatchByName(string batchName,
        CancellationToken cancellationToken = default)
    {
        return GetAsyncReturn<BatchDto>(
            CrawlerServiceApiRoutes.BatchRoute.BatchBase + CrawlerServiceApiRoutes.BatchRoute.GetByName + "/?name=" +
            Uri.EscapeDataString(batchName), false, cancellationToken);
    }

    public Task<Result<BatchDto>> CreateBatch(BatchDto batch, CancellationToken cancellationToken = default)
    {
        return PostAsyncReturn<BatchDto>(
            CrawlerServiceApiRoutes.BatchRoute.BatchBase + CrawlerServiceApiRoutes.BatchRoute.Create, false,
            JsonSerializer.Serialize(batch), cancellationToken);
    }

    public Task<Result> UpdateBatch(BatchDto batch, CancellationToken cancellationToken = default)
    {
        return PutAsync(CrawlerServiceApiRoutes.BatchRoute.BatchBase + CrawlerServiceApiRoutes.BatchRoute.Update,
            JsonSerializer.Serialize(batch), cancellationToken);
    }

    public ValueTask<Result> DeleteBatch(string batchName, CancellationToken cancellationToken = default)
    {
        return DeleteAsync(
            CrawlerServiceApiRoutes.BatchRoute.BatchBase + CrawlerServiceApiRoutes.BatchRoute.Delete + "/?name=" +
            Uri.EscapeDataString(batchName), cancellationToken);
    }

    public Task<Result<List<string>>> GetHostStartUrlNamesByBatch(string batchName,
        CancellationToken cancellationToken = default)
    {
        return GetAsyncReturn<List<string>>(
            CrawlerServiceApiRoutes.BatchRoute.BatchBase + CrawlerServiceApiRoutes.BatchRoute.HostByBatchList +
            "/?batchName=" + Uri.EscapeDataString(batchName), false, cancellationToken);
    }

    public ValueTask<Result> AddHostByBatch(HostByBatchRequest request,
        CancellationToken cancellationToken = default)
    {
        return PostAsync(
            CrawlerServiceApiRoutes.BatchRoute.BatchBase + CrawlerServiceApiRoutes.BatchRoute.HostByBatchAdd, false,
            JsonSerializer.Serialize(request), cancellationToken);
    }

    public ValueTask<Result> RemoveHostByBatch(string batchName, string schemeName, string hostName,
        CancellationToken cancellationToken = default)
    {
        return DeleteAsync(
            CrawlerServiceApiRoutes.BatchRoute.BatchBase + CrawlerServiceApiRoutes.BatchRoute.HostByBatchRemove +
            "/?batchName=" + Uri.EscapeDataString(batchName) + "&schemeName=" + Uri.EscapeDataString(schemeName) +
            "&hostName=" + Uri.EscapeDataString(hostName), cancellationToken);
    }

    public Task<Result<List<HostDto>>> GetHostsList(CancellationToken cancellationToken = default)
    {
        return GetAsyncReturn<List<HostDto>>(
            CrawlerServiceApiRoutes.HostRoute.HostBase + CrawlerServiceApiRoutes.HostRoute.List, false,
            cancellationToken);
    }

    public async Task<Result<HostDto?>> GetHostByName(string hostName,
        CancellationToken cancellationToken = default)
    {
        Result<ApiNullableResult<HostDto>> result = await GetAsyncReturn<ApiNullableResult<HostDto>>(
            CrawlerServiceApiRoutes.HostRoute.HostBase + CrawlerServiceApiRoutes.HostRoute.GetByName + "/?name=" +
            Uri.EscapeDataString(hostName), false, cancellationToken);
        return result.IsFailure ? Result.Failure<HostDto?>(result.Error) : Result.Success(result.Value.Value);
    }

    public Task<Result<HostDto>> CreateHost(HostDto host, CancellationToken cancellationToken = default)
    {
        return PostAsyncReturn<HostDto>(
            CrawlerServiceApiRoutes.HostRoute.HostBase + CrawlerServiceApiRoutes.HostRoute.Create, false,
            JsonSerializer.Serialize(host), cancellationToken);
    }

    public Task<Result> UpdateHost(HostDto host, CancellationToken cancellationToken = default)
    {
        return PutAsync(CrawlerServiceApiRoutes.HostRoute.HostBase + CrawlerServiceApiRoutes.HostRoute.Update,
            JsonSerializer.Serialize(host), cancellationToken);
    }

    public ValueTask<Result> DeleteHost(string hostName, CancellationToken cancellationToken = default)
    {
        return DeleteAsync(
            CrawlerServiceApiRoutes.HostRoute.HostBase + CrawlerServiceApiRoutes.HostRoute.Delete + "/?name=" +
            Uri.EscapeDataString(hostName), cancellationToken);
    }

    public Task<Result<List<SchemeDto>>> GetSchemesList(CancellationToken cancellationToken = default)
    {
        return GetAsyncReturn<List<SchemeDto>>(
            CrawlerServiceApiRoutes.SchemeRoute.SchemeBase + CrawlerServiceApiRoutes.SchemeRoute.List, false,
            cancellationToken);
    }

    public async Task<Result<SchemeDto?>> GetSchemeByName(string schemeName,
        CancellationToken cancellationToken = default)
    {
        Result<ApiNullableResult<SchemeDto>> result = await GetAsyncReturn<ApiNullableResult<SchemeDto>>(
            CrawlerServiceApiRoutes.SchemeRoute.SchemeBase + CrawlerServiceApiRoutes.SchemeRoute.GetByName + "/?name=" +
            Uri.EscapeDataString(schemeName), false, cancellationToken);
        return result.IsFailure ? Result.Failure<SchemeDto?>(result.Error) : Result.Success(result.Value.Value);
    }

    public Task<Result<SchemeDto>> CreateScheme(SchemeDto scheme,
        CancellationToken cancellationToken = default)
    {
        return PostAsyncReturn<SchemeDto>(
            CrawlerServiceApiRoutes.SchemeRoute.SchemeBase + CrawlerServiceApiRoutes.SchemeRoute.Create, false,
            JsonSerializer.Serialize(scheme), cancellationToken);
    }

    public Task<Result> UpdateScheme(SchemeDto scheme, CancellationToken cancellationToken = default)
    {
        return PutAsync(CrawlerServiceApiRoutes.SchemeRoute.SchemeBase + CrawlerServiceApiRoutes.SchemeRoute.Update,
            JsonSerializer.Serialize(scheme), cancellationToken);
    }

    public ValueTask<Result> DeleteScheme(string schemeName, CancellationToken cancellationToken = default)
    {
        return DeleteAsync(
            CrawlerServiceApiRoutes.SchemeRoute.SchemeBase + CrawlerServiceApiRoutes.SchemeRoute.Delete + "/?name=" +
            Uri.EscapeDataString(schemeName), cancellationToken);
    }

    public Task<Result<List<TaskDto>>> GetTasksList(CancellationToken cancellationToken = default)
    {
        return GetAsyncReturn<List<TaskDto>>(
            CrawlerServiceApiRoutes.TaskRoute.TaskBase + CrawlerServiceApiRoutes.TaskRoute.List, false,
            cancellationToken);
    }

    public async Task<Result<TaskDto?>> GetTaskByName(string taskName,
        CancellationToken cancellationToken = default)
    {
        Result<ApiNullableResult<TaskDto>> result = await GetAsyncReturn<ApiNullableResult<TaskDto>>(
            CrawlerServiceApiRoutes.TaskRoute.TaskBase + CrawlerServiceApiRoutes.TaskRoute.GetByName + "/?name=" +
            Uri.EscapeDataString(taskName), false, cancellationToken);
        return result.IsFailure ? Result.Failure<TaskDto?>(result.Error) : Result.Success(result.Value.Value);
    }

    public Task<Result<TaskDto>> CreateTask(TaskDto task, CancellationToken cancellationToken = default)
    {
        return PostAsyncReturn<TaskDto>(
            CrawlerServiceApiRoutes.TaskRoute.TaskBase + CrawlerServiceApiRoutes.TaskRoute.Create, false,
            JsonSerializer.Serialize(task), cancellationToken);
    }

    public Task<Result> UpdateTask(TaskDto task, CancellationToken cancellationToken = default)
    {
        return PutAsync(CrawlerServiceApiRoutes.TaskRoute.TaskBase + CrawlerServiceApiRoutes.TaskRoute.Update,
            JsonSerializer.Serialize(task), cancellationToken);
    }

    public ValueTask<Result> DeleteTask(string taskName, CancellationToken cancellationToken = default)
    {
        return DeleteAsync(
            CrawlerServiceApiRoutes.TaskRoute.TaskBase + CrawlerServiceApiRoutes.TaskRoute.Delete + "/?name=" +
            Uri.EscapeDataString(taskName), cancellationToken);
    }

    public ValueTask<Result> ClearTaskFetchedData(string taskName,
        CancellationToken cancellationToken = default)
    {
        return DeleteAsync(
            CrawlerServiceApiRoutes.TaskRoute.TaskBase + CrawlerServiceApiRoutes.TaskRoute.ClearFetchedData +
            "/?name=" + Uri.EscapeDataString(taskName), cancellationToken);
    }

    public async Task<Result<TaskStartPointDto?>> GetStartPoint(int taskId, string startPoint,
        CancellationToken cancellationToken = default)
    {
        Result<ApiNullableResult<TaskStartPointDto>> result =
            await GetAsyncReturn<ApiNullableResult<TaskStartPointDto>>(
                CrawlerServiceApiRoutes.TaskRoute.TaskBase + CrawlerServiceApiRoutes.TaskRoute.StartPointGet +
                "/?taskId=" + taskId + "&startPoint=" + Uri.EscapeDataString(startPoint), false, cancellationToken);
        return result.IsFailure ? Result.Failure<TaskStartPointDto?>(result.Error) : Result.Success(result.Value.Value);
    }

    public Task<Result<TaskStartPointDto>> AddStartPoint(AddStartPointRequest request,
        CancellationToken cancellationToken = default)
    {
        return PostAsyncReturn<TaskStartPointDto>(
            CrawlerServiceApiRoutes.TaskRoute.TaskBase + CrawlerServiceApiRoutes.TaskRoute.StartPointAdd, false,
            JsonSerializer.Serialize(request), cancellationToken);
    }

    public Task<Result> UpdateStartPoint(TaskStartPointDto startPoint,
        CancellationToken cancellationToken = default)
    {
        return PutAsync(CrawlerServiceApiRoutes.TaskRoute.TaskBase + CrawlerServiceApiRoutes.TaskRoute.StartPointUpdate,
            JsonSerializer.Serialize(startPoint), cancellationToken);
    }

    public ValueTask<Result> DeleteStartPoint(int taskId, string startPoint,
        CancellationToken cancellationToken = default)
    {
        return DeleteAsync(
            CrawlerServiceApiRoutes.TaskRoute.TaskBase + CrawlerServiceApiRoutes.TaskRoute.StartPointDelete +
            "/?taskId=" + taskId + "&startPoint=" + Uri.EscapeDataString(startPoint), cancellationToken);
    }
}
