namespace CrawlerServiceShared.Contracts;

public sealed class RunTaskRequest
{
    public string? TaskName { get; set; }
    public int NewPartsCreateLimit { get; set; }

    //პროგრესის შეტყობინებების გაგზავნებს შორის მინიმალური დაყოვნება წამებში
    public int ProgressDelaySeconds { get; set; } = 1;
}
