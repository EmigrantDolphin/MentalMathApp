using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MentalMathApp.LevelConfigurations.Custom;
using MentalMathApp.LevelConfigurations.Enums;
using MentalMathApp.Services.CustomManager;
using System.ComponentModel;

namespace MentalMathApp.ViewModels.NumberCustom;

public enum NumberCustomConfigurationParameters { MutableConfiguration }
public partial class NumberCustomConfigurationViewModel : NumberCustomBaseViewModel, IQueryAttributable, INotifyPropertyChanged
{
    private MutableCustomConfiguration _configuration;
    private readonly ICustomLevelManager _levelManager;

    public NumberCustomConfigurationViewModel(ICustomLevelManager levelManager)
    {
        _levelManager = levelManager;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.ContainsKey(nameof(NumberCustomConfigurationParameters.MutableConfiguration)))
        {
            return;
        }

        _configuration = query[nameof(NumberCustomConfigurationParameters.MutableConfiguration)] as MutableCustomConfiguration;

        SecondsPerEquation = _configuration.SecondsPerEquation;
        NumberOfEquations = _configuration.NumberOfEquations;
        AvailableNumberOperations = new NumberOperations[] { NumberOperations.Addition, NumberOperations.Subtraction, NumberOperations.Multiplication, NumberOperations.Division };
        SelectedNumberOperations = _configuration.Operations;
    }

    [ObservableProperty]
    private int secondsPerEquation;

    [ObservableProperty]
    private int numberOfEquations;

    [ObservableProperty]
    private NumberOperations[] availableNumberOperations;

    [ObservableProperty]
    private NumberOperations[] selectedNumberOperations;

    [RelayCommand]
    private void SaveChanges()
    {
        _configuration.MutableSecondsPerEquation = SecondsPerEquation;
        _configuration.MutableNumberOfEquations = NumberOfEquations;

        // TODO: Figure out why selected operations are not getting updated
        _levelManager.UpdateMutableConfigurations();
    }
}
