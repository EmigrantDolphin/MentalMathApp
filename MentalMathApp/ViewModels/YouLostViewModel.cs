using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MentalMathApp.LevelConfigurations;
using MentalMathApp.LevelConfigurations.Enums;
using MentalMathApp.Navigation;
using MentalMathApp.Services.CustomManager;
using MentalMathApp.Services.EquationFormers.Models;

namespace MentalMathApp.ViewModels;

public enum YouLostQueryAttributes
{
    TimeExpired,
    WrongAnswer,
    Configuration
}
public record WrongAnswerData(string Equation, PossibleAnswer WrongAnswer, string CorrectAnswer);

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
            WrongAnswer = wrongAnswerData.WrongAnswer.Answer;
            WrongHiddenAnswer = wrongAnswerData.WrongAnswer.HiddenAnswer;
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
    private string wrongHiddenAnswer = "";
    [ObservableProperty]
    private string correctAnswer = "";
    private NumberConfigurationBase _configuration;

    [RelayCommand]
    private async Task TryAgainAsync()
    {
        IsBusy = true;
        await Navigate.GoBackAsync();
    }

    [RelayCommand]
    private async Task GoToMenuAsync()
    {
        IsBusy = true;
        if (_configuration.GameType == GameType.Story)
        {
            await Navigate.ToStoryMenuAsync();
        }

        if (_configuration.GameType == GameType.Custom)
        {
            await Navigate.ToNumberCustomMenuAsync(_configuration.NumberType);
        }
    }
}
