using MentalMathApp.LevelConfigurations.Enums;

namespace MentalMathApp.LevelConfigurations;

public abstract class NumberConfigurationBase
{
    public abstract string LevelName { get; }
    public abstract int SecondsPerEquation { get; }
    public abstract int NumberOfEquations { get; }
    public abstract NumberOperations[] Operations { get; }
    public abstract int IntervalFrom { get; }
    public abstract int IntervalTo { get; }
    public abstract GameType GameType { get; }
    public abstract NumberTypes NumberType { get; }
    public abstract string NextLevelName { get; }

    public string ConfigurationKey => $"{LevelName}_{GameType}_{NumberType}";
}
