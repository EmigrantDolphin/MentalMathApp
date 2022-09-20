using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MentalMathApp.LevelConfigurations.Custom;
using MentalMathApp.LevelConfigurations.Enums;
using MentalMathApp.Services.CustomManager;
using System.Collections.ObjectModel;
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

        AvailableNumberOperationss.Add(NumberOperations.Addition);
        AvailableNumberOperationss.Add(NumberOperations.Subtraction);
        AvailableNumberOperationss.Add(NumberOperations.Division);
        AvailableNumberOperationss.Add(NumberOperations.Multiplication);

        foreach (var operation in _configuration.Operations)
        {
            SelectedNumberOperations.Add(operation);
        }
    }

    [ObservableProperty]
    private int secondsPerEquation;

    [ObservableProperty]
    private int numberOfEquations;

    public ObservableCollection<object> AvailableNumberOperationss { get; set; } = new();

    private ObservableCollection<object> selectedNumberOperations = new();
    public ObservableCollection<object> SelectedNumberOperations
    {
        get
        {
            return selectedNumberOperations;
        }
        set
        {
            if (selectedNumberOperations != value)
            {
                selectedNumberOperations = value;
            }
        }
    }

    [RelayCommand]
    private void SaveChanges()
    {
        _configuration.MutableSecondsPerEquation = SecondsPerEquation;
        _configuration.MutableNumberOfEquations = NumberOfEquations;
        _configuration.MutableOperations = SelectedNumberOperations.Select(x => (NumberOperations) x).ToArray();

        _levelManager.UpdateMutableConfigurations();

        GoBack();
    }
}
