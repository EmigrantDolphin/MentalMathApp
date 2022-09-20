using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MentalMathApp.LevelConfigurations;
using MentalMathApp.LevelConfigurations.Enums;
using MentalMathApp.LevelConfigurations.Models;
using MentalMathApp.Navigation;
using MentalMathApp.Services.CustomManager;
using MentalMathApp.Services.StoryManager;

namespace MentalMathApp.ViewModels;

public enum YouWonQueryParameters
{
    CurrentLevelConfiguration,
    AverageSecondsPerLevel
}

public partial class YouWonViewModel : BaseViewModel, IQueryAttributable
{
    private readonly ILevelManager _levelManager;
    private readonly IHistoryManager _historyManager;
    private NumberConfigurationBase _beatenConfiguration;
    private string _nextLevel = string.Empty;

    public YouWonViewModel(ILevelManager levelManager, IHistoryManager historyManager)
    {
        _levelManager = levelManager;
        _historyManager = historyManager;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.ContainsKey(nameof(YouWonQueryParameters.CurrentLevelConfiguration)))
        {
            return;
        }

        _beatenConfiguration = query[nameof(YouWonQueryParameters.CurrentLevelConfiguration)] as NumberConfigurationBase;
        CurrentLevel = new(_beatenConfiguration.LevelName, _beatenConfiguration.NumberType);

        if (_beatenConfiguration.GameType == GameType.Story)
        {
            _nextLevel = _beatenConfiguration.NextLevelName;
            if (_nextLevel is not null)
            {
                HasNextLevel = true;
            }
            _levelManager.LevelBeaten(CurrentLevel);
        }

        if (_beatenConfiguration.GameType == GameType.Custom)
        {
            decimal? averageSecondsPerEquation = null;
            if (query.ContainsKey(nameof(YouWonQueryParameters.AverageSecondsPerLevel)))
            {
                averageSecondsPerEquation = query[nameof(YouWonQueryParameters.AverageSecondsPerLevel)] as decimal?;
            }
            _historyManager.AppendHistory(_beatenConfiguration, true, averageSecondsPerEquation);
        }
    }

    [ObservableProperty]
    private bool hasNextLevel = false;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(BeatenLevelText))]
    private LevelDetails currentLevel;

    public string BeatenLevelText => $"You've beaten level {currentLevel?.Name}!";

    [RelayCommand]
    private async Task NextLevel()
    {
        if (_nextLevel is null)
        {
            return;
        }

        var configuration = _levelManager.GetConfiguration(new(_nextLevel, CurrentLevel.NumberType));

        await Navigate.ToNumberGame(configuration);
    }

    [RelayCommand]
    private async Task AgainAsync()
    {
        await Navigate.GoBack();
    }

    [RelayCommand]
    private async Task GoToMenuAsync()
    {
        IsBusy = true;

        if (_beatenConfiguration.GameType == GameType.Story)
        {
            await Navigate.ToStoryMenu();
        }
        
        if (_beatenConfiguration.GameType == GameType.Custom)
        {
            await Navigate.ToNumberCustomMenu(_beatenConfiguration.NumberType);
        }

        IsBusy = false;
    }
}
