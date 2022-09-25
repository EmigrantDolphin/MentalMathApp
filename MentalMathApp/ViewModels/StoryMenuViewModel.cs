using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MentalMathApp.LevelConfigurations.Enums;
using MentalMathApp.LevelConfigurations.Models;
using MentalMathApp.Navigation;
using MentalMathApp.Services.StoryManager;

namespace MentalMathApp.ViewModels;

public record LevelModel(string LevelName, bool IsBeaten, bool IsAvailable, NumberTypes Type) // TODO: move to a separate file
{
    public LevelDetails ToLevelDetails() => new (LevelName, Type);
    public static List<LevelModel> ToModel(List<LevelDetails> allLevels, List<string> beatenLevels)
    {
        var lvls = (from all in allLevels
                    join beaten in beatenLevels on all.Name equals beaten into joined
                    from rightBeaten in joined.DefaultIfEmpty()
                    select new LevelModel(all.Name, rightBeaten is not null, rightBeaten is not null, all.NumberType)).ToArray();

        for (int i = 0; i < lvls.Length; i++)
        {
            if (!lvls[i].IsBeaten && !lvls[i].IsAvailable)
            {
                lvls[i] = lvls[i] with { IsAvailable = true };
                break;
            }
        }

        return lvls.ToList();
    }
}

public partial class StoryMenuViewModel : BaseViewModel
{
    private readonly ILevelManager _levelManager;
    public StoryMenuViewModel(ILevelManager levelManager)
    {
        _levelManager = levelManager;
    }

    public void LoadLevels()
    {
        var integerLevels = _levelManager.GetLevels(NumberTypes.Integer);
        var beatenIntegerLevels = _levelManager.GetBeatenLevels(NumberTypes.Integer);

        var rationalLevels = _levelManager.GetLevels(NumberTypes.Rational);
        var beatenRationalLevels = _levelManager.GetBeatenLevels(NumberTypes.Rational);

        IntegerLevels = LevelModel.ToModel(integerLevels, beatenIntegerLevels).ToArray();
        RationalLevels = LevelModel.ToModel(rationalLevels, beatenRationalLevels).ToArray();
        IsBusy = false;
    }

    [ObservableProperty]
    LevelModel[] integerLevels;

    [ObservableProperty]
    LevelModel[] rationalLevels;

    [RelayCommand]
    private void GoToSelectedLevel(LevelModel level)
    {
        IsBusy = true;
        var levelDetails = level.ToLevelDetails();
        var configuration = _levelManager.GetConfiguration(levelDetails);
        _ = Navigate.ToNumberGameAsync(configuration);
    }

    [RelayCommand]
    private async Task ResetStory()
    {
        IsBusy = true;

        var accepted = await Shell.Current.DisplayAlert("Reset story", "Are you sure you want to reset story?", "Yes", "No");
        if (!accepted)
        {
            return;
        }

        await _levelManager.ResetStory();
        LoadLevels();

        IsBusy = false;
    }
}
