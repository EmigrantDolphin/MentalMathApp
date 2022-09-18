using MentalMathApp.LevelConfigurations;
using MentalMathApp.LevelConfigurations.Enums;
using MentalMathApp.LevelConfigurations.Models;
using MentalMathApp.LevelConfigurations.Story;
using MentalMathApp.Navigation;
using MentalMathApp.Services.Storage;

namespace MentalMathApp.Services.StoryManager;

public interface ILevelManager
{
    List<LevelDetails> GetLevels(NumberTypes numberType);
    List<string> GetBeatenLevels(NumberTypes type);
    NumberConfigurationBase GetConfiguration(LevelDetails level);
    void LevelBeaten(LevelDetails level);
    Task ResetStory();
}

public class StoryLevelManager : ILevelManager
{
    private readonly IDeviceStorage _storage;
    private readonly IStoryConfigurationFactory _storyConfigurationFactory;

    private List<string> _beatenIntegerLevels = new();
    private List<string> _beatenRationalLevels = new();

    public StoryLevelManager(IDeviceStorage storage, IStoryConfigurationFactory storyConfigurationFactory)
    {
        _storage = storage;
        _storyConfigurationFactory = storyConfigurationFactory;

        _ = LoadProgress();
    }

    public List<LevelDetails> GetLevels(NumberTypes numberType) => _storyConfigurationFactory.GetLevels(numberType);

    public NumberConfigurationBase GetConfiguration(LevelDetails level) =>
        _storyConfigurationFactory.GetConfiguration(level);

    public void LevelBeaten(LevelDetails level)
    {
        //TODO: save levelDetails objects
        if (level.NumberType == NumberTypes.Integer)
        {
            var alreadyBeaten = _beatenIntegerLevels.Any(x => x.Equals(level.Name));
            if (alreadyBeaten) return;

            _beatenIntegerLevels.Add(level.Name);

            _storage.SaveBeatenIntegerLevelNames(_beatenIntegerLevels);
        }

        if (level.NumberType == NumberTypes.Rational)
        {
            var alreadyBeaten = _beatenRationalLevels.Any(x => x.Equals(level.Name));
            if (alreadyBeaten) return;

            _beatenRationalLevels.Add(level.Name);

            _storage.SaveBeatenRationalLevelNames(_beatenIntegerLevels);
        }
    }

    public List<string> GetBeatenLevels(NumberTypes type) => type switch
    {
        NumberTypes.Integer => _beatenIntegerLevels,
        NumberTypes.Rational => _beatenRationalLevels,
        _ => throw new NotImplementedException("Tried getting beaten level. Supplied number type is not implemented")
    };

    public async Task ResetStory()
    {
        _storage.ClearStoryProgress();
        await LoadProgress();
        await Navigate.ToStoryMenu();
    }

    private async Task LoadProgress()
    {
        _beatenIntegerLevels = await _storage.GetBeatenLevelNames(NumberTypes.Integer);
        _beatenRationalLevels = await _storage.GetBeatenLevelNames(NumberTypes.Rational);
    }
}
