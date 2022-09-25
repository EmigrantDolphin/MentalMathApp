using MentalMathApp.LevelConfigurations;
using MentalMathApp.LevelConfigurations.Custom;
using MentalMathApp.LevelConfigurations.Enums;
using MentalMathApp.Services.EquationFormers.Models;
using MentalMathApp.ViewModels;
using MentalMathApp.ViewModels.NumberCustom;
using MentalMathApp.Views;

namespace MentalMathApp.Navigation;

public static class Navigate
{
    public static async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..", false);
    }

    public static async Task ToStoryMenuAsync()
    {
        await Shell.Current.GoToAsync(nameof(StoryMenu), false);
    }

    public static async Task ToMainMenuAsync()
    {
        await Shell.Current.GoToAsync(nameof(MainMenu), false);
    }

    public static async Task ToNumberCustomMenuAsync(NumberTypes type)
    {
        var parameters = new Dictionary<string, object>
        {
            {nameof(NumberCustomMenuParameters.NumberType), type }
        };

        await Shell.Current.GoToAsync(nameof(NumberCustomMenu), false, parameters);
    }

    public static async Task ToNumberGameAsync(NumberConfigurationBase configuration)
    {
        if (configuration is null)
        {
            return;
        }

        var parameters = new Dictionary<string, object>
        {
            {nameof(NumberGameQueryProps.LevelConfiguration), configuration }
        };

        await Shell.Current.GoToAsync(nameof(NumberGame), false, parameters);
    }

    public static async Task ToCustomLevelConfigurationAsync(MutableCustomConfiguration configuration)
    {
        var parameters = new Dictionary<string, object>
        {
            {nameof(NumberCustomConfigurationParameters.MutableConfiguration), configuration }
        };

        await Shell.Current.GoToAsync(nameof(NumberCustomLevelConfiguration), false, parameters);
    }

    public static async Task ToCustomLevelHistoryAsync(NumberConfigurationBase configuration)
    {
        var parameters = new Dictionary<string, object>
        {
            {nameof(NumberCustomLevelHistoryParameters.Configuration), configuration }
        };

        await Shell.Current.GoToAsync(nameof(NumberCustomLevelHistory), false, parameters);
    }

    public static async Task ToYouLostWrongAnswerAsync(string equation, PossibleAnswer answer, string correctAnswer, NumberConfigurationBase configuration)
    {
        var parameters = new Dictionary<string, object>()
        {
            {nameof(YouLostQueryAttributes.WrongAnswer), new WrongAnswerData(equation, answer, correctAnswer) },
            {nameof(YouLostQueryAttributes.Configuration), configuration }
        };

        await Shell.Current.GoToAsync(nameof(YouLost), false, parameters);
    }

    public static async Task ToYouLostTimeExpiredAsync(NumberConfigurationBase configuration)
    {
        var parameters = new Dictionary<string, object>
        {
            {nameof(YouLostQueryAttributes.TimeExpired), true },
            {nameof(YouLostQueryAttributes.Configuration), configuration }
        };

        await Shell.Current.GoToAsync(nameof(YouLost), false, parameters);
    }

    public static async Task ToYouWonAsync(NumberConfigurationBase configuration, decimal averageSecondsPerEquation)
    {
        var parameters = new Dictionary<string, object>
        {
            {nameof(YouWonQueryParameters.CurrentLevelConfiguration), configuration },
            {nameof(YouWonQueryParameters.AverageSecondsPerLevel), averageSecondsPerEquation }
        };
        await Shell.Current.GoToAsync(nameof(YouWon), false, parameters);
    }
}
