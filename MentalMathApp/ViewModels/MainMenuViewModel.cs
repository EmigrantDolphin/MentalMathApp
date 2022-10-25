using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MentalMathApp.LevelConfigurations;
using MentalMathApp.LevelConfigurations.Enums;
using MentalMathApp.Navigation;
using MentalMathApp.Services.CustomManager;

namespace MentalMathApp.ViewModels;

public partial class MainMenuViewModel : BaseViewModel
{
    private readonly ICustomLevelManager _customLevelManager;

    private NumberConfigurationBase _lastPlayedCustomConfiguration;

    public MainMenuViewModel(ICustomLevelManager customLevelManager)
    {
        _customLevelManager = customLevelManager;
        IsLastPlayedConfigurationAvailable = false;

        _ = LoadLastPlayedConfiguration();
    }

    //NOTE: interesting xaml behavior when using IsEnabled and Command on a button. IsEnabled has to be after Command for it to work
    //Otherwise some sort of CanExecute mechanism overrides IsEnabled and this property gets ignored
    [ObservableProperty]
    private bool isLastPlayedConfigurationAvailable;

    [RelayCommand]
    public async Task GoToIntegerNumberCustomMenuAsync()
    {
        await Navigate.ToNumberCustomMenuAsync(NumberTypes.Integer);
    }

    [RelayCommand]
    public async Task GoToRationalNumberCustomMenuAsync()
    {
        await Navigate.ToNumberCustomMenuAsync(NumberTypes.Rational);
    }

    [RelayCommand]
    public async Task GoToLastPlayedConfigurationGameAsync()
    {
        await Navigate.ToNumberGameAsync(_lastPlayedCustomConfiguration);
    }

    [RelayCommand]
    public void ShowCompeteMessage()
    {
        Shell.Current.DisplayAlert("Compete online with others!", "This feature is not developed." +
            "But it will if the app reaches at least 10k downloads by 2023." +
            "You will be able to create/join public games and compete to learn who solves equations faster!", "Ok, I will tell my friends :)");
    }

    private async Task LoadLastPlayedConfiguration()
    {
        var config = await _customLevelManager.GetLastPlayedConfiguration();
        if (config is null)
        {
            return;
        }

        _lastPlayedCustomConfiguration = config;
        IsLastPlayedConfigurationAvailable = true;
    }
}
