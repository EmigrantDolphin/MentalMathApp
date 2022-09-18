﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MentalMathApp.LevelConfigurations;
using MentalMathApp.LevelConfigurations.Custom;
using MentalMathApp.LevelConfigurations.Enums;
using MentalMathApp.Navigation;
using MentalMathApp.Services.CustomManager;

namespace MentalMathApp.ViewModels.NumberCustom;

public record NumberCustomLevels(string Name, MutableCustomConfiguration Configuration);

public enum NumberCustomMenuParameters { NumberType }

public partial class NumberCustomMenuViewModel : NumberCustomBaseViewModel, IQueryAttributable
{
    private readonly ICustomLevelManager _levelManager;
    private NumberTypes _numberType;

    public NumberCustomMenuViewModel(ICustomLevelManager levelManager)
    {
        _levelManager = levelManager;
    }


    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.ContainsKey(nameof(NumberCustomMenuParameters.NumberType)))
        {
            return;
        }

        _numberType = (query[nameof(NumberCustomMenuParameters.NumberType)] as NumberTypes?).Value;

        var configurations = _levelManager.GetConfigurations(_numberType);
        Levels = configurations.Select(x => new NumberCustomLevels(x.LevelName, x)).ToArray();
    }

    [ObservableProperty]
    private NumberCustomLevels[] levels;

    [RelayCommand]
    private void GoToSelectedLevel(NumberConfigurationBase configuration)
    {
        Navigate.ToNumberGame(configuration);
    }

    [RelayCommand]
    private void GoToConfiguration(MutableCustomConfiguration configuration)
    {
        Navigate.ToCustomLevelConfiguration(configuration);
    }

    [RelayCommand]
    private void GoToHistory(NumberConfigurationBase configuration)
    {
        Navigate.ToCustomLevelHistory(configuration);
    }
}