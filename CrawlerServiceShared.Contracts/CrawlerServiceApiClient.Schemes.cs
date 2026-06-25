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
    public Task<OneOf<List<SchemeDto>, Error[]>> GetSchemesList(CancellationToken cancellationToken = default)
    {
        return GetAsyncReturn<List<SchemeDto>>(
            CrawlerServiceApiRoutes.SchemeRoute.SchemeBase + CrawlerServiceApiRoutes.SchemeRoute.List, false,
            cancellationToken);
    }

    public async Task<OneOf<SchemeDto?, Error[]>> GetSchemeByName(string schemeName,
        CancellationToken cancellationToken = default)
    {
        OneOf<ApiNullableResult<SchemeDto>, Error[]> result = await GetAsyncReturn<ApiNullableResult<SchemeDto>>(
            CrawlerServiceApiRoutes.SchemeRoute.SchemeBase + CrawlerServiceApiRoutes.SchemeRoute.GetByName + "/?name=" +
            Uri.EscapeDataString(schemeName), false, cancellationToken);
        return result.Match<OneOf<SchemeDto?, Error[]>>(wrapper => wrapper.Value, errors => errors);
    }

    public Task<OneOf<SchemeDto, Error[]>> CreateScheme(SchemeDto scheme, CancellationToken cancellationToken = default)
    {
        return PostAsyncReturn<SchemeDto>(
            CrawlerServiceApiRoutes.SchemeRoute.SchemeBase + CrawlerServiceApiRoutes.SchemeRoute.Create, false,
            JsonSerializer.Serialize(scheme), cancellationToken);
    }

    public Task<Option<Error[]>> UpdateScheme(SchemeDto scheme, CancellationToken cancellationToken = default)
    {
        return PutAsync(CrawlerServiceApiRoutes.SchemeRoute.SchemeBase + CrawlerServiceApiRoutes.SchemeRoute.Update,
            JsonSerializer.Serialize(scheme), cancellationToken);
    }

    public ValueTask<Option<Error[]>> DeleteScheme(string schemeName, CancellationToken cancellationToken = default)
    {
        return DeleteAsync(
            CrawlerServiceApiRoutes.SchemeRoute.SchemeBase + CrawlerServiceApiRoutes.SchemeRoute.Delete + "/?name=" +
            Uri.EscapeDataString(schemeName), cancellationToken);
    }
}
