using MentalMathApp.LevelConfigurations.Enums;
using Newtonsoft.Json;

namespace MentalMathApp.LevelConfigurations.Custom;

public class MutableCustomConfiguration : NumberConfigurationBase
{
    public MutableCustomConfiguration()
    {
        // for deserializing
    }

    public MutableCustomConfiguration(NumberConfigurationBase configuration)
    {
        MutableLevelName = configuration.LevelName;
        MutableSecondsPerEquation = configuration.SecondsPerEquation;
        MutableNumberOfEquations = configuration.NumberOfEquations;
        MutableOperations = configuration.Operations;
        MutableIntervalFrom = configuration.IntervalFrom;
        MutableIntervalTo = configuration.IntervalTo;
        MutableGameType = configuration.GameType;
        MutableNumberType = configuration.NumberType;
        MutableNextLevelName = configuration.NextLevelName;
    }

    public string MutableLevelName { get; set; }

    public int MutableSecondsPerEquation { get; set; }

    public int MutableNumberOfEquations { get; set; }

    public NumberOperations[] MutableOperations { get; set; }

    public int MutableIntervalFrom { get; set; }

    public int MutableIntervalTo { get; set; }

    public GameType MutableGameType { get; set; }

    public NumberTypes MutableNumberType { get; set; }

    public string MutableNextLevelName { get; set; }


    [JsonIgnore]
    public override string LevelName => MutableLevelName;

    [JsonIgnore]
    public override int SecondsPerEquation => MutableSecondsPerEquation;

    [JsonIgnore]
    public override int NumberOfEquations => MutableNumberOfEquations;

    [JsonIgnore]
    public override NumberOperations[] Operations => MutableOperations;

    [JsonIgnore]
    public override int IntervalFrom => MutableIntervalFrom;

    [JsonIgnore]
    public override int IntervalTo => MutableIntervalTo;

    [JsonIgnore]
    public override GameType GameType => MutableGameType;

    [JsonIgnore]
    public override NumberTypes NumberType => MutableNumberType;

    [JsonIgnore]
    public override string NextLevelName => MutableNextLevelName;
}
