namespace CrawlerServiceShared.Contracts;

public sealed class TestOnePageRequest
{
    public string? TaskName { get; set; }
    public string? Url { get; set; }
    public bool DeleteContentForReanalyze { get; set; }
    public int NewPartsCreateLimit { get; set; }

    //პროგრესის შეტყობინებების გაგზავნებს შორის მინიმალური დაყოვნება წამებში
    public int ProgressDelaySeconds { get; set; } = 1;
}
