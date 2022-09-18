using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MentalMathApp.LevelConfigurations;
using MentalMathApp.LevelConfigurations.Enums;
using MentalMathApp.Navigation;
using MentalMathApp.Services.CustomManager;

namespace MentalMathApp.ViewModels;

public enum YouLostQueryAttributes
{
    TimeExpired,
    WrongAnswer,
    Configuration
}
public record WrongAnswerData(string Equation, string WrongAnswer, string CorrectAnswer);

public partial class YouLostViewModel : BaseViewModel, IQueryAttributable
{
    private readonly IHistoryManager _historyManager;

    public YouLostViewModel(IHistoryManager historyManager)
    {
        _historyManager = historyManager;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey(nameof(YouLostQueryAttributes.TimeExpired)))
        {
            IsTimeOver = (query[nameof(YouLostQueryAttributes.TimeExpired)] as bool?).Value;
        }

        if (query.ContainsKey(nameof(YouLostQueryAttributes.WrongAnswer)))
        {
            var wrongAnswerData = query[nameof(YouLostQueryAttributes.WrongAnswer)] as WrongAnswerData;
            Equation = wrongAnswerData.Equation;
            WrongAnswer = wrongAnswerData.WrongAnswer;
            CorrectAnswer = wrongAnswerData.CorrectAnswer;
            IsWrongAnswer = true;
        }

        if (query.ContainsKey(nameof(YouLostQueryAttributes.Configuration)))
        {
            _configuration = query[nameof(YouLostQueryAttributes.Configuration)] as NumberConfigurationBase;

            if (_configuration.GameType != GameType.Custom)
            {
                return;
            }

            _historyManager.AppendHistory(_configuration, false, null);
        }
    }

    [ObservableProperty]
    private bool isTimeOver = false;

    [ObservableProperty]
    private bool isWrongAnswer = false;

    [ObservableProperty]
    private string equation = "";
    [ObservableProperty]
    private string wrongAnswer = "";
    [ObservableProperty]
    private string correctAnswer = "";
    private NumberConfigurationBase _configuration;

    [RelayCommand]
    private void TryAgain()
    {
        Navigate.GoBack();
    }

    [RelayCommand]
    private void GoToMenu()
    {
        if (_configuration.GameType == GameType.Story)
        {
            _ = Navigate.ToStoryMenu();
        }

        if (_configuration.GameType == GameType.Custom)
        {
            Navigate.ToNumberCustomMenu(_configuration.NumberType);
        }
    }
}
