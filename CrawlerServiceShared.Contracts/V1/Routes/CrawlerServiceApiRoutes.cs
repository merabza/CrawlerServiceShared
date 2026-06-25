namespace CrawlerServiceShared.Contracts.V1.Routes;

public static class CrawlerServiceApiRoutes
{
    private const string Api = "api";
    private const string Version = "v1";
    public const string ApiBase = Api + "/" + Version;

    public static class CrawlerRoute
    {
        public const string CrawlerBase = "/crawler";

        // POST api/v1/crawler/runbatch
        public const string RunBatch = "/runbatch";

        // POST api/v1/crawler/runtask
        public const string RunTask = "/runtask";

        // POST api/v1/crawler/testonepage
        public const string TestOnePage = "/testonepage";

        // GET api/v1/crawler/precheck
        public const string PreCheck = "/precheck";
    }

    public static class TaskRoute
    {
        public const string TaskBase = "/tasks";

        public const string List = "/list";
        public const string GetByName = "/getbyname";
        public const string Create = "/create";
        public const string Update = "/update";
        public const string Delete = "/delete";

        public const string StartPointGet = "/startpointget";
        public const string StartPointAdd = "/startpointadd";
        public const string StartPointUpdate = "/startpointupdate";
        public const string StartPointDelete = "/startpointdelete";
    }

    public static class BatchRoute
    {
        public const string BatchBase = "/batches";

        public const string List = "/list";
        public const string GetByName = "/getbyname";
        public const string Create = "/create";
        public const string Update = "/update";
        public const string Delete = "/delete";

        public const string HostByBatchList = "/hostbybatchlist";
        public const string HostByBatchAdd = "/hostbybatchadd";
        public const string HostByBatchRemove = "/hostbybatchremove";
    }

    public static class HostRoute
    {
        public const string HostBase = "/hosts";

        public const string List = "/list";
        public const string GetByName = "/getbyname";
        public const string Create = "/create";
        public const string Update = "/update";
        public const string Delete = "/delete";
    }

    public static class SchemeRoute
    {
        public const string SchemeBase = "/schemes";

        public const string List = "/list";
        public const string GetByName = "/getbyname";
        public const string Create = "/create";
        public const string Update = "/update";
        public const string Delete = "/delete";
    }
}
