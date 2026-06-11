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
    }
}
