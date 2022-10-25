using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MentalMathApp.LevelConfigurations;
using MentalMathApp.LevelConfigurations.Enums;
using MentalMathApp.LevelConfigurations.Models;
using MentalMathApp.Navigation;
using MentalMathApp.Services.CustomManager;
using MentalMathApp.Services.EquationFormers;
using MentalMathApp.Services.EquationFormers.Models;

namespace MentalMathApp.ViewModels;

public enum NumberGameQueryProps { LevelConfiguration };

public partial class NumberGameViewModel : BaseViewModel, IQueryAttributable
{
    private readonly ICustomLevelManager _customLevelManager;

    private NumberConfigurationBase _numberConfiguration;
    private NumberEquationFormer _numberEquationFormer;

    private string _correctAnswer;
    private int _numberOfEquations = 0;
    private List<int> _secondsPerLevel = new();

    public NumberGameViewModel(ICustomLevelManager customLevelManager)
    {
        _customLevelManager = customLevelManager;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        _numberConfiguration = query[nameof(NumberGameQueryProps.LevelConfiguration)] as NumberConfigurationBase;
        SelectedLevel = new LevelDetails(_numberConfiguration.LevelName, _numberConfiguration.NumberType);

        _numberEquationFormer = new NumberEquationFormer(_numberConfiguration);
        _numberOfEquations = _numberConfiguration.NumberOfEquations;
        EquationsAnswered = 0;

        SetNewEquation();

        if (_numberConfiguration.GameType == GameType.Custom)
        {
            _customLevelManager.ConfigurationPlayed(_numberConfiguration);
        }
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SelectedLevelText))]
    public LevelDetails selectedLevel;

    public string SelectedLevelText => "Level " + selectedLevel?.Name;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(EquationsAnsweredText))]
    public string equation;

    [ObservableProperty]
    public PossibleAnswer[] answers;

    [ObservableProperty]
    public int equationsAnswered;
    public string EquationsAnsweredText => $"{equationsAnswered}/{_numberOfEquations} equations";

    [ObservableProperty]
    public int timer;

    [RelayCommand]
    private void SetNewEquation()
    {
        var equation = _numberEquationFormer.NewEquation(SelectedLevel.NumberType);

        Equation = equation.Formula;
        Answers = equation.PossibleAnswers;
        _correctAnswer = equation.Answer;

        CancelTasks();
        StartCountdown(_numberConfiguration.SecondsPerEquation);
    }

    [RelayCommand]
    private async Task SubmitAnswerAsync(PossibleAnswer answer)
    {
        if (answer.Answer.Equals(_correctAnswer))
        {
            EquationsAnswered++;
            _secondsPerLevel.Add(_numberConfiguration.SecondsPerEquation - Timer);

            if (EquationsAnswered == _numberOfEquations)
            {
                var averageSecondsPerEquation = (decimal)_secondsPerLevel.Average();
                averageSecondsPerEquation = decimal.Round(averageSecondsPerEquation, 2, MidpointRounding.AwayFromZero);
                await Navigate.ToYouWonAsync(_numberConfiguration, averageSecondsPerEquation);
                return;
            }
            SetNewEquation();
        }
        else
        {
            await Navigate.ToYouLostWrongAnswerAsync(Equation, answer, _correctAnswer, _numberConfiguration);
        }
    }

    private void StartCountdown(int seconds)
    {
        var ct = CreateCancellationToken();

        _ = Task.Run(async () =>
        {
            Timer = seconds;

            while (seconds > 0)
            {
                if (ct.IsCancellationRequested)
                {
                    return;
                }

                await Task.Delay(TimeSpan.FromSeconds(1), ct);
                seconds -= 1;
                Timer = seconds;
            }

            if (EquationsAnswered < _numberOfEquations)
            {
                // await Navigate.ToYouLostTimeExpiredAsync(_numberConfiguration);
            }
        }, ct);
    }

    public void OnDisappearing()
    {
        CancelTasks();
    }
}
