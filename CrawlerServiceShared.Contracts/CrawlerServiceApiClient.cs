using System;
using System.Collections.Generic;
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

    public ValueTask<Option<ErrorOmd[]>> RunBatch(string batchName, int newPartsCreateLimit, int progressDelaySeconds,
        CancellationToken cancellationToken = default)
    {
        return PostAsync(
            CrawlerServiceApiRoutes.CrawlerRoute.CrawlerBase + CrawlerServiceApiRoutes.CrawlerRoute.RunBatch +
            "/?batchName=" + Uri.EscapeDataString(batchName) + "&newPartsCreateLimit=" + newPartsCreateLimit +
            "&progressDelaySeconds=" + progressDelaySeconds, cancellationToken);
    }

    public Task<OneOf<CrawlerPreCheckResult, ErrorOmd[]>> PreCheck(string name, string? pageAddress,
        CancellationToken cancellationToken = default)
    {
        string afterServerAddress = CrawlerServiceApiRoutes.CrawlerRoute.CrawlerBase +
                                    CrawlerServiceApiRoutes.CrawlerRoute.PreCheck + "/?batchName=" +
                                    Uri.EscapeDataString(name) + (string.IsNullOrWhiteSpace(pageAddress)
                                        ? string.Empty
                                        : "&url=" + Uri.EscapeDataString(pageAddress));
        return GetAsyncReturn<CrawlerPreCheckResult>(afterServerAddress, false, cancellationToken);
    }

    public ValueTask<Option<ErrorOmd[]>> RunTask(RunTaskRequest request, CancellationToken cancellationToken = default)
    {
        return PostAsync(
            CrawlerServiceApiRoutes.CrawlerRoute.CrawlerBase + CrawlerServiceApiRoutes.CrawlerRoute.RunTask, true,
            JsonSerializer.Serialize(request), cancellationToken);
    }

    public ValueTask<Option<ErrorOmd[]>> TestOnePage(TestOnePageRequest request,
        CancellationToken cancellationToken = default)
    {
        return PostAsync(
            CrawlerServiceApiRoutes.CrawlerRoute.CrawlerBase + CrawlerServiceApiRoutes.CrawlerRoute.TestOnePage, true,
            JsonSerializer.Serialize(request), cancellationToken);
    }

    public Task<OneOf<List<BatchDto>, ErrorOmd[]>> GetBatchesList(CancellationToken cancellationToken = default)
    {
        return GetAsyncReturn<List<BatchDto>>(
            CrawlerServiceApiRoutes.BatchRoute.BatchBase + CrawlerServiceApiRoutes.BatchRoute.List, false,
            cancellationToken);
    }

    public async Task<OneOf<BatchDto?, ErrorOmd[]>> GetBatchByName(string batchName,
        CancellationToken cancellationToken = default)
    {
        OneOf<ApiNullableResult<BatchDto>, ErrorOmd[]> result = await GetAsyncReturn<ApiNullableResult<BatchDto>>(
            CrawlerServiceApiRoutes.BatchRoute.BatchBase + CrawlerServiceApiRoutes.BatchRoute.GetByName + "/?name=" +
            Uri.EscapeDataString(batchName), false, cancellationToken);
        return result.Match<OneOf<BatchDto?, ErrorOmd[]>>(wrapper => wrapper.Value, errors => errors);
    }

    public Task<OneOf<BatchDto, ErrorOmd[]>> CreateBatch(BatchDto batch, CancellationToken cancellationToken = default)
    {
        return PostAsyncReturn<BatchDto>(
            CrawlerServiceApiRoutes.BatchRoute.BatchBase + CrawlerServiceApiRoutes.BatchRoute.Create, false,
            JsonSerializer.Serialize(batch), cancellationToken);
    }

    public Task<Option<ErrorOmd[]>> UpdateBatch(BatchDto batch, CancellationToken cancellationToken = default)
    {
        return PutAsync(CrawlerServiceApiRoutes.BatchRoute.BatchBase + CrawlerServiceApiRoutes.BatchRoute.Update,
            JsonSerializer.Serialize(batch), cancellationToken);
    }

    public ValueTask<Option<ErrorOmd[]>> DeleteBatch(string batchName, CancellationToken cancellationToken = default)
    {
        return DeleteAsync(
            CrawlerServiceApiRoutes.BatchRoute.BatchBase + CrawlerServiceApiRoutes.BatchRoute.Delete + "/?name=" +
            Uri.EscapeDataString(batchName), cancellationToken);
    }

    public Task<OneOf<List<string>, ErrorOmd[]>> GetHostStartUrlNamesByBatch(string batchName,
        CancellationToken cancellationToken = default)
    {
        return GetAsyncReturn<List<string>>(
            CrawlerServiceApiRoutes.BatchRoute.BatchBase + CrawlerServiceApiRoutes.BatchRoute.HostByBatchList +
            "/?batchName=" + Uri.EscapeDataString(batchName), false, cancellationToken);
    }

    public ValueTask<Option<ErrorOmd[]>> AddHostByBatch(HostByBatchRequest request,
        CancellationToken cancellationToken = default)
    {
        return PostAsync(
            CrawlerServiceApiRoutes.BatchRoute.BatchBase + CrawlerServiceApiRoutes.BatchRoute.HostByBatchAdd, false,
            JsonSerializer.Serialize(request), cancellationToken);
    }

    public ValueTask<Option<ErrorOmd[]>> RemoveHostByBatch(string batchName, string schemeName, string hostName,
        CancellationToken cancellationToken = default)
    {
        return DeleteAsync(
            CrawlerServiceApiRoutes.BatchRoute.BatchBase + CrawlerServiceApiRoutes.BatchRoute.HostByBatchRemove +
            "/?batchName=" + Uri.EscapeDataString(batchName) + "&schemeName=" + Uri.EscapeDataString(schemeName) +
            "&hostName=" + Uri.EscapeDataString(hostName), cancellationToken);
    }

    public Task<OneOf<List<HostDto>, ErrorOmd[]>> GetHostsList(CancellationToken cancellationToken = default)
    {
        return GetAsyncReturn<List<HostDto>>(
            CrawlerServiceApiRoutes.HostRoute.HostBase + CrawlerServiceApiRoutes.HostRoute.List, false,
            cancellationToken);
    }

    public async Task<OneOf<HostDto?, ErrorOmd[]>> GetHostByName(string hostName,
        CancellationToken cancellationToken = default)
    {
        OneOf<ApiNullableResult<HostDto>, ErrorOmd[]> result = await GetAsyncReturn<ApiNullableResult<HostDto>>(
            CrawlerServiceApiRoutes.HostRoute.HostBase + CrawlerServiceApiRoutes.HostRoute.GetByName + "/?name=" +
            Uri.EscapeDataString(hostName), false, cancellationToken);
        return result.Match<OneOf<HostDto?, ErrorOmd[]>>(wrapper => wrapper.Value, errors => errors);
    }

    public Task<OneOf<HostDto, ErrorOmd[]>> CreateHost(HostDto host, CancellationToken cancellationToken = default)
    {
        return PostAsyncReturn<HostDto>(
            CrawlerServiceApiRoutes.HostRoute.HostBase + CrawlerServiceApiRoutes.HostRoute.Create, false,
            JsonSerializer.Serialize(host), cancellationToken);
    }

    public Task<Option<ErrorOmd[]>> UpdateHost(HostDto host, CancellationToken cancellationToken = default)
    {
        return PutAsync(CrawlerServiceApiRoutes.HostRoute.HostBase + CrawlerServiceApiRoutes.HostRoute.Update,
            JsonSerializer.Serialize(host), cancellationToken);
    }

    public ValueTask<Option<ErrorOmd[]>> DeleteHost(string hostName, CancellationToken cancellationToken = default)
    {
        return DeleteAsync(
            CrawlerServiceApiRoutes.HostRoute.HostBase + CrawlerServiceApiRoutes.HostRoute.Delete + "/?name=" +
            Uri.EscapeDataString(hostName), cancellationToken);
    }

    public Task<OneOf<List<SchemeDto>, ErrorOmd[]>> GetSchemesList(CancellationToken cancellationToken = default)
    {
        return GetAsyncReturn<List<SchemeDto>>(
            CrawlerServiceApiRoutes.SchemeRoute.SchemeBase + CrawlerServiceApiRoutes.SchemeRoute.List, false,
            cancellationToken);
    }

    public async Task<OneOf<SchemeDto?, ErrorOmd[]>> GetSchemeByName(string schemeName,
        CancellationToken cancellationToken = default)
    {
        OneOf<ApiNullableResult<SchemeDto>, ErrorOmd[]> result = await GetAsyncReturn<ApiNullableResult<SchemeDto>>(
            CrawlerServiceApiRoutes.SchemeRoute.SchemeBase + CrawlerServiceApiRoutes.SchemeRoute.GetByName + "/?name=" +
            Uri.EscapeDataString(schemeName), false, cancellationToken);
        return result.Match<OneOf<SchemeDto?, ErrorOmd[]>>(wrapper => wrapper.Value, errors => errors);
    }

    public Task<OneOf<SchemeDto, ErrorOmd[]>> CreateScheme(SchemeDto scheme,
        CancellationToken cancellationToken = default)
    {
        return PostAsyncReturn<SchemeDto>(
            CrawlerServiceApiRoutes.SchemeRoute.SchemeBase + CrawlerServiceApiRoutes.SchemeRoute.Create, false,
            JsonSerializer.Serialize(scheme), cancellationToken);
    }

    public Task<Option<ErrorOmd[]>> UpdateScheme(SchemeDto scheme, CancellationToken cancellationToken = default)
    {
        return PutAsync(CrawlerServiceApiRoutes.SchemeRoute.SchemeBase + CrawlerServiceApiRoutes.SchemeRoute.Update,
            JsonSerializer.Serialize(scheme), cancellationToken);
    }

    public ValueTask<Option<ErrorOmd[]>> DeleteScheme(string schemeName, CancellationToken cancellationToken = default)
    {
        return DeleteAsync(
            CrawlerServiceApiRoutes.SchemeRoute.SchemeBase + CrawlerServiceApiRoutes.SchemeRoute.Delete + "/?name=" +
            Uri.EscapeDataString(schemeName), cancellationToken);
    }

    public Task<OneOf<List<TaskDto>, ErrorOmd[]>> GetTasksList(CancellationToken cancellationToken = default)
    {
        return GetAsyncReturn<List<TaskDto>>(
            CrawlerServiceApiRoutes.TaskRoute.TaskBase + CrawlerServiceApiRoutes.TaskRoute.List, false,
            cancellationToken);
    }

    public async Task<OneOf<TaskDto?, ErrorOmd[]>> GetTaskByName(string taskName,
        CancellationToken cancellationToken = default)
    {
        OneOf<ApiNullableResult<TaskDto>, ErrorOmd[]> result = await GetAsyncReturn<ApiNullableResult<TaskDto>>(
            CrawlerServiceApiRoutes.TaskRoute.TaskBase + CrawlerServiceApiRoutes.TaskRoute.GetByName + "/?name=" +
            Uri.EscapeDataString(taskName), false, cancellationToken);
        return result.Match<OneOf<TaskDto?, ErrorOmd[]>>(wrapper => wrapper.Value, errors => errors);
    }

    public Task<OneOf<TaskDto, ErrorOmd[]>> CreateTask(TaskDto task, CancellationToken cancellationToken = default)
    {
        return PostAsyncReturn<TaskDto>(
            CrawlerServiceApiRoutes.TaskRoute.TaskBase + CrawlerServiceApiRoutes.TaskRoute.Create, false,
            JsonSerializer.Serialize(task), cancellationToken);
    }

    public Task<Option<ErrorOmd[]>> UpdateTask(TaskDto task, CancellationToken cancellationToken = default)
    {
        return PutAsync(CrawlerServiceApiRoutes.TaskRoute.TaskBase + CrawlerServiceApiRoutes.TaskRoute.Update,
            JsonSerializer.Serialize(task), cancellationToken);
    }

    public ValueTask<Option<ErrorOmd[]>> DeleteTask(string taskName, CancellationToken cancellationToken = default)
    {
        return DeleteAsync(
            CrawlerServiceApiRoutes.TaskRoute.TaskBase + CrawlerServiceApiRoutes.TaskRoute.Delete + "/?name=" +
            Uri.EscapeDataString(taskName), cancellationToken);
    }

    public ValueTask<Option<ErrorOmd[]>> ClearTaskFetchedData(string taskName,
        CancellationToken cancellationToken = default)
    {
        return DeleteAsync(
            CrawlerServiceApiRoutes.TaskRoute.TaskBase + CrawlerServiceApiRoutes.TaskRoute.ClearFetchedData +
            "/?name=" + Uri.EscapeDataString(taskName), cancellationToken);
    }

    public async Task<OneOf<TaskStartPointDto?, ErrorOmd[]>> GetStartPoint(int taskId, string startPoint,
        CancellationToken cancellationToken = default)
    {
        OneOf<ApiNullableResult<TaskStartPointDto>, ErrorOmd[]> result =
            await GetAsyncReturn<ApiNullableResult<TaskStartPointDto>>(
                CrawlerServiceApiRoutes.TaskRoute.TaskBase + CrawlerServiceApiRoutes.TaskRoute.StartPointGet +
                "/?taskId=" + taskId + "&startPoint=" + Uri.EscapeDataString(startPoint), false, cancellationToken);
        return result.Match<OneOf<TaskStartPointDto?, ErrorOmd[]>>(wrapper => wrapper.Value, errors => errors);
    }

    public Task<OneOf<TaskStartPointDto, ErrorOmd[]>> AddStartPoint(AddStartPointRequest request,
        CancellationToken cancellationToken = default)
    {
        return PostAsyncReturn<TaskStartPointDto>(
            CrawlerServiceApiRoutes.TaskRoute.TaskBase + CrawlerServiceApiRoutes.TaskRoute.StartPointAdd, false,
            JsonSerializer.Serialize(request), cancellationToken);
    }

    public Task<Option<ErrorOmd[]>> UpdateStartPoint(TaskStartPointDto startPoint,
        CancellationToken cancellationToken = default)
    {
        return PutAsync(CrawlerServiceApiRoutes.TaskRoute.TaskBase + CrawlerServiceApiRoutes.TaskRoute.StartPointUpdate,
            JsonSerializer.Serialize(startPoint), cancellationToken);
    }

    public ValueTask<Option<ErrorOmd[]>> DeleteStartPoint(int taskId, string startPoint,
        CancellationToken cancellationToken = default)
    {
        return DeleteAsync(
            CrawlerServiceApiRoutes.TaskRoute.TaskBase + CrawlerServiceApiRoutes.TaskRoute.StartPointDelete +
            "/?taskId=" + taskId + "&startPoint=" + Uri.EscapeDataString(startPoint), cancellationToken);
    }
}
