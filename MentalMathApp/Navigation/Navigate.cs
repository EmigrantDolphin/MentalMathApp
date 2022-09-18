using MentalMathApp.LevelConfigurations;
using MentalMathApp.LevelConfigurations.Custom;
using MentalMathApp.LevelConfigurations.Enums;
using MentalMathApp.ViewModels;
using MentalMathApp.ViewModels.NumberCustom;
using MentalMathApp.Views;

namespace MentalMathApp.Navigation;

public static class Navigate
{
    public static void GoBack()
    {
        Shell.Current.GoToAsync("..", false);
    }

    public static async Task ToStoryMenu()
    {
        await Shell.Current.GoToAsync(nameof(StoryMenu), false);
    }

    public static void ToMainMenu()
    {
        Shell.Current.GoToAsync(nameof(MainMenu), false);
    }

    public static void ToNumberCustomMenu(NumberTypes type)
    {
        var parameters = new Dictionary<string, object>
        {
            {nameof(NumberCustomMenuParameters.NumberType), type }
        };

        Shell.Current.GoToAsync(nameof(NumberCustomMenu), false, parameters);
    }

    public static void ToNumberGame(NumberConfigurationBase configuration)
    {
        if (configuration is null)
        {
            return;
        }

        var parameters = new Dictionary<string, object>
        {
            {nameof(NumberGameQueryProps.LevelConfiguration), configuration }
        };

        Shell.Current.GoToAsync(nameof(NumberGame), false, parameters);
    }

    public static void ToCustomLevelConfiguration(MutableCustomConfiguration configuration)
    {
        var parameters = new Dictionary<string, object>
        {
            {nameof(NumberCustomConfigurationParameters.MutableConfiguration), configuration }
        };

        Shell.Current.GoToAsync(nameof(NumberCustomLevelConfiguration), false, parameters);
    }

    public static void ToCustomLevelHistory(NumberConfigurationBase configuration)
    {
        var parameters = new Dictionary<string, object>
        {
            {nameof(NumberCustomLevelHistoryParameters.Configuration), configuration }
        };

        Shell.Current.GoToAsync(nameof(NumberCustomLevelHistory), false, parameters);
    }

    public static void ToYouLostWrongAnswer(string equation, string answer, string correctAnswer, NumberConfigurationBase configuration)
    {
        var parameters = new Dictionary<string, object>()
        {
            {nameof(YouLostQueryAttributes.WrongAnswer), new WrongAnswerData(equation, answer, correctAnswer) },
            {nameof(YouLostQueryAttributes.Configuration), configuration }
        };

        Shell.Current.GoToAsync(nameof(YouLost), false, parameters);
    }

    public static void ToYouLostTimeExpired(NumberConfigurationBase configuration)
    {
        var parameters = new Dictionary<string, object>
        {
            {nameof(YouLostQueryAttributes.TimeExpired), true },
            {nameof(YouLostQueryAttributes.Configuration), configuration }
        };

        _ = Shell.Current.GoToAsync(nameof(YouLost), false, parameters);
    }

    public static void ToYouWon(NumberConfigurationBase configuration, decimal averageSecondsPerEquation)
    {
        var parameters = new Dictionary<string, object>
        {
            {nameof(YouWonQueryParameters.CurrentLevelConfiguration), configuration },
            {nameof(YouWonQueryParameters.AverageSecondsPerLevel), averageSecondsPerEquation }
        };
        Shell.Current.GoToAsync(nameof(YouWon), false, parameters);
    }
}
