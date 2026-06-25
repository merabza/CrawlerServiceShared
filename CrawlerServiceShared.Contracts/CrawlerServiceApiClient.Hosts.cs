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
    public Task<OneOf<List<HostDto>, Error[]>> GetHostsList(CancellationToken cancellationToken = default)
    {
        return GetAsyncReturn<List<HostDto>>(
            CrawlerServiceApiRoutes.HostRoute.HostBase + CrawlerServiceApiRoutes.HostRoute.List, false,
            cancellationToken);
    }

    public async Task<OneOf<HostDto?, Error[]>> GetHostByName(string hostName,
        CancellationToken cancellationToken = default)
    {
        OneOf<ApiNullableResult<HostDto>, Error[]> result = await GetAsyncReturn<ApiNullableResult<HostDto>>(
            CrawlerServiceApiRoutes.HostRoute.HostBase + CrawlerServiceApiRoutes.HostRoute.GetByName + "/?name=" +
            Uri.EscapeDataString(hostName), false, cancellationToken);
        return result.Match<OneOf<HostDto?, Error[]>>(wrapper => wrapper.Value, errors => errors);
    }

    public Task<OneOf<HostDto, Error[]>> CreateHost(HostDto host, CancellationToken cancellationToken = default)
    {
        return PostAsyncReturn<HostDto>(
            CrawlerServiceApiRoutes.HostRoute.HostBase + CrawlerServiceApiRoutes.HostRoute.Create, false,
            JsonSerializer.Serialize(host), cancellationToken);
    }

    public Task<Option<Error[]>> UpdateHost(HostDto host, CancellationToken cancellationToken = default)
    {
        return PutAsync(CrawlerServiceApiRoutes.HostRoute.HostBase + CrawlerServiceApiRoutes.HostRoute.Update,
            JsonSerializer.Serialize(host), cancellationToken);
    }

    public ValueTask<Option<Error[]>> DeleteHost(string hostName, CancellationToken cancellationToken = default)
    {
        return DeleteAsync(
            CrawlerServiceApiRoutes.HostRoute.HostBase + CrawlerServiceApiRoutes.HostRoute.Delete + "/?name=" +
            Uri.EscapeDataString(hostName), cancellationToken);
    }
}
