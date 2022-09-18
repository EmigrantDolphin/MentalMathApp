namespace MentalMathApp.Services.CustomManager.Models;

public record HistoryModel
{
    public HistoryModel()
    {
        Date = DateTimeOffset.UtcNow;
    }

    public string Operations { get; init; }
    public int SecondsPerEquation { get; init; }
    public int EquationsPerGame { get; init; }

    public bool Won { get; init; }
    public decimal? AverageSecondsPerEquation { get; init; }
    public decimal? ImprovementInSeconds { get; init; }
    public DateTimeOffset Date { get; init; } 
}
