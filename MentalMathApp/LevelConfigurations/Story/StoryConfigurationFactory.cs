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
        IntegerLevel4Configuration.Name,
        IntegerLevel5Configuration.Name,
        IntegerLevel6Configuration.Name,
        IntegerLevel7Configuration.Name,
        IntegerLevel8Configuration.Name,
        IntegerLevel9Configuration.Name,
        IntegerLevel10Configuration.Name,
        IntegerLevel11Configuration.Name,
        IntegerLevel12Configuration.Name,
        IntegerLevel13Configuration.Name,
        IntegerLevel14Configuration.Name,
        IntegerLevel15Configuration.Name,
        IntegerLevel16Configuration.Name,
        IntegerLevel17Configuration.Name
    };

    private readonly List<string> _decimalLevelNames = new()
    {

        RationalLevel1Configuration.Name,
        RationalLevel2Configuration.Name,
        RationalLevel3Configuration.Name,
        RationalLevel4Configuration.Name,
        RationalLevel5Configuration.Name,
        RationalLevel6Configuration.Name,
        RationalLevel7Configuration.Name,
        RationalLevel8Configuration.Name,
        RationalLevel9Configuration.Name,
        RationalLevel10Configuration.Name,
        RationalLevel11Configuration.Name,
        RationalLevel12Configuration.Name,
        RationalLevel13Configuration.Name,
        RationalLevel14Configuration.Name
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
                IntegerLevel5Configuration.Name => new IntegerLevel5Configuration(),
                IntegerLevel6Configuration.Name => new IntegerLevel6Configuration(),
                IntegerLevel7Configuration.Name => new IntegerLevel7Configuration(),
                IntegerLevel8Configuration.Name => new IntegerLevel8Configuration(),
                IntegerLevel9Configuration.Name => new IntegerLevel9Configuration(),
                IntegerLevel10Configuration.Name => new IntegerLevel10Configuration(),
                IntegerLevel11Configuration.Name => new IntegerLevel11Configuration(),
                IntegerLevel12Configuration.Name => new IntegerLevel12Configuration(),
                IntegerLevel13Configuration.Name => new IntegerLevel13Configuration(),
                IntegerLevel14Configuration.Name => new IntegerLevel14Configuration(),
                IntegerLevel15Configuration.Name => new IntegerLevel15Configuration(),
                IntegerLevel16Configuration.Name => new IntegerLevel16Configuration(),
                IntegerLevel17Configuration.Name => new IntegerLevel17Configuration(),
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
                RationalLevel5Configuration.Name => new RationalLevel5Configuration(),
                RationalLevel6Configuration.Name => new RationalLevel6Configuration(),
                RationalLevel7Configuration.Name => new RationalLevel7Configuration(),
                RationalLevel8Configuration.Name => new RationalLevel8Configuration(),
                RationalLevel9Configuration.Name => new RationalLevel9Configuration(),
                RationalLevel10Configuration.Name => new RationalLevel10Configuration(),
                RationalLevel11Configuration.Name => new RationalLevel11Configuration(),
                RationalLevel12Configuration.Name => new RationalLevel12Configuration(),
                RationalLevel13Configuration.Name => new RationalLevel13Configuration(),
                RationalLevel14Configuration.Name => new RationalLevel14Configuration(),
                _ => throw new Exception("No such rational level")
            };
        }

        return null;
    }
}
