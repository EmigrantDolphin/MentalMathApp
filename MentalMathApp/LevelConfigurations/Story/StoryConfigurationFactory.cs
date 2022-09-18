using MentalMathApp.LevelConfigurations.Enums;
using MentalMathApp.LevelConfigurations.Models;
using MentalMathApp.LevelConfigurations.Story.Integer;
using MentalMathApp.LevelConfigurations.Story.Rational;

namespace MentalMathApp.LevelConfigurations.Story;

public interface IStoryConfigurationFactory
{
    List<LevelDetails> GetLevels(NumberTypes type);
    NumberConfigurationBase GetConfiguration(LevelDetails level);
}

public class StoryConfigurationFactory : IStoryConfigurationFactory
{
    private readonly List<string> _integerLevelNames = new()
    {
        IntegerLevel1Configuration.Name,
        IntegerLevel2Configuration.Name,
        IntegerLevel3Configuration.Name,
        IntegerLevel4Configuration.Name
    };

    private readonly List<string> _decimalLevelNames = new()
    {

        RationalLevel1Configuration.Name,
        RationalLevel2Configuration.Name,
        RationalLevel3Configuration.Name,
        RationalLevel4Configuration.Name
    };

    public List<LevelDetails> GetLevels(NumberTypes type)
    {
        var levelNames = type switch
        {
            NumberTypes.Integer => _integerLevelNames,
            NumberTypes.Rational => _decimalLevelNames,
        _ => throw new Exception($"Enum constant not defined in switch of type: {nameof(NumberTypes)}, {type}")
        };

        return levelNames.Select(name => new LevelDetails(name, type)).ToList();
    }

    public NumberConfigurationBase GetConfiguration(LevelDetails level)
    {
        if (level.NumberType == NumberTypes.Integer)
        {
            return level.Name switch
            {
                IntegerLevel1Configuration.Name => new IntegerLevel1Configuration(),
                IntegerLevel2Configuration.Name => new IntegerLevel2Configuration(),
                IntegerLevel3Configuration.Name => new IntegerLevel3Configuration(),
                IntegerLevel4Configuration.Name => new IntegerLevel4Configuration(),
                _ => throw new Exception("No such integer level")
            };
        }

        if (level.NumberType == NumberTypes.Rational)
        {
            return level.Name switch
            {
                RationalLevel1Configuration.Name => new RationalLevel1Configuration(),
                RationalLevel2Configuration.Name => new RationalLevel2Configuration(),
                RationalLevel3Configuration.Name => new RationalLevel3Configuration(),
                RationalLevel4Configuration.Name => new RationalLevel4Configuration(),
                _ => throw new Exception("No such rational level")
            };
        }

        return null;
    }
}
