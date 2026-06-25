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
    public Task<OneOf<List<TaskDto>, Error[]>> GetTasksList(CancellationToken cancellationToken = default)
    {
        return GetAsyncReturn<List<TaskDto>>(
            CrawlerServiceApiRoutes.TaskRoute.TaskBase + CrawlerServiceApiRoutes.TaskRoute.List, false,
            cancellationToken);
    }

    public async Task<OneOf<TaskDto?, Error[]>> GetTaskByName(string taskName,
        CancellationToken cancellationToken = default)
    {
        OneOf<ApiNullableResult<TaskDto>, Error[]> result = await GetAsyncReturn<ApiNullableResult<TaskDto>>(
            CrawlerServiceApiRoutes.TaskRoute.TaskBase + CrawlerServiceApiRoutes.TaskRoute.GetByName + "/?name=" +
            Uri.EscapeDataString(taskName), false, cancellationToken);
        return result.Match<OneOf<TaskDto?, Error[]>>(wrapper => wrapper.Value, errors => errors);
    }

    public Task<OneOf<TaskDto, Error[]>> CreateTask(TaskDto task, CancellationToken cancellationToken = default)
    {
        return PostAsyncReturn<TaskDto>(
            CrawlerServiceApiRoutes.TaskRoute.TaskBase + CrawlerServiceApiRoutes.TaskRoute.Create, false,
            JsonSerializer.Serialize(task), cancellationToken);
    }

    public Task<Option<Error[]>> UpdateTask(TaskDto task, CancellationToken cancellationToken = default)
    {
        return PutAsync(CrawlerServiceApiRoutes.TaskRoute.TaskBase + CrawlerServiceApiRoutes.TaskRoute.Update,
            JsonSerializer.Serialize(task), cancellationToken);
    }

    public ValueTask<Option<Error[]>> DeleteTask(string taskName, CancellationToken cancellationToken = default)
    {
        return DeleteAsync(
            CrawlerServiceApiRoutes.TaskRoute.TaskBase + CrawlerServiceApiRoutes.TaskRoute.Delete + "/?name=" +
            Uri.EscapeDataString(taskName), cancellationToken);
    }

    public async Task<OneOf<TaskStartPointDto?, Error[]>> GetStartPoint(int taskId, string startPoint,
        CancellationToken cancellationToken = default)
    {
        OneOf<ApiNullableResult<TaskStartPointDto>, Error[]> result =
            await GetAsyncReturn<ApiNullableResult<TaskStartPointDto>>(
                CrawlerServiceApiRoutes.TaskRoute.TaskBase + CrawlerServiceApiRoutes.TaskRoute.StartPointGet +
                "/?taskId=" + taskId + "&startPoint=" + Uri.EscapeDataString(startPoint), false, cancellationToken);
        return result.Match<OneOf<TaskStartPointDto?, Error[]>>(wrapper => wrapper.Value, errors => errors);
    }

    public Task<OneOf<TaskStartPointDto, Error[]>> AddStartPoint(AddStartPointRequest request,
        CancellationToken cancellationToken = default)
    {
        return PostAsyncReturn<TaskStartPointDto>(
            CrawlerServiceApiRoutes.TaskRoute.TaskBase + CrawlerServiceApiRoutes.TaskRoute.StartPointAdd, false,
            JsonSerializer.Serialize(request), cancellationToken);
    }

    public Task<Option<Error[]>> UpdateStartPoint(TaskStartPointDto startPoint,
        CancellationToken cancellationToken = default)
    {
        return PutAsync(
            CrawlerServiceApiRoutes.TaskRoute.TaskBase + CrawlerServiceApiRoutes.TaskRoute.StartPointUpdate,
            JsonSerializer.Serialize(startPoint), cancellationToken);
    }

    public ValueTask<Option<Error[]>> DeleteStartPoint(int taskId, string startPoint,
        CancellationToken cancellationToken = default)
    {
        return DeleteAsync(
            CrawlerServiceApiRoutes.TaskRoute.TaskBase + CrawlerServiceApiRoutes.TaskRoute.StartPointDelete +
            "/?taskId=" + taskId + "&startPoint=" + Uri.EscapeDataString(startPoint), cancellationToken);
    }
}
