using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MentalMathApp.LevelConfigurations;
using MentalMathApp.Services.CustomManager;
using MentalMathApp.Services.CustomManager.Models;

namespace MentalMathApp.ViewModels.NumberCustom;

public enum NumberCustomLevelHistoryParameters
{
    Configuration
}

public record HistoryDetails(
    string Operations,
    int SecondsPerEquation,
    int EquationsPerGame,
    bool Won,
    decimal? AverageSecondsPerEquation,
    string ImprovementInSeconds,
    bool IsImprovementPositive 
    )
{
    public static List<HistoryDetails> ToModel(List<HistoryModel> models)
    {
        return models.Select(x => new HistoryDetails(
            x.Operations,
            x.SecondsPerEquation,
            x.EquationsPerGame,
            x.Won,
            x.AverageSecondsPerEquation,
            x.ImprovementInSeconds is not null && x.ImprovementInSeconds.Value >= 0 ? $"+{x.ImprovementInSeconds.Value}" : x.ImprovementInSeconds?.ToString(),
            x.ImprovementInSeconds is not null && x.ImprovementInSeconds.Value >= 0
            )).ToList();
    }
}

public partial class NumberCustomLevelHistoryViewModel : NumberCustomBaseViewModel, IQueryAttributable
{
    private readonly IHistoryManager _historyManager;
    private NumberConfigurationBase _configuration;

    public NumberCustomLevelHistoryViewModel(IHistoryManager historyManager)
    {
        _historyManager = historyManager;
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.ContainsKey(nameof(NumberCustomLevelHistoryParameters.Configuration)))
        {
            return;
        }

        _configuration = query[nameof(NumberCustomLevelHistoryParameters.Configuration)] as NumberConfigurationBase;

        var historyModels = (await _historyManager.GetHistoryModels(_configuration.ConfigurationKey));
        HistoryDetailsArray = HistoryDetails.ToModel(historyModels);
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsHistoryEmpty))]
    private List<HistoryDetails> historyDetailsArray = new();

    public bool IsHistoryEmpty => !HistoryDetailsArray.Any();

    [RelayCommand]
    private async Task ResetHistory()
    {
        IsBusy = true;

        var shouldClear = await Shell.Current.DisplayAlert("Reset history", "Do you really wish to do that?", "Yep", "Whoops");
        if (shouldClear)
        {
            HistoryDetailsArray = new();
            _historyManager.ClearHistory(_configuration);
        }

        IsBusy = false;
    }
}
