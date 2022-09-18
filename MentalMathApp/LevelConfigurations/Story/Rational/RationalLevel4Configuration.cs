using MentalMathApp.LevelConfigurations.Enums;

namespace MentalMathApp.LevelConfigurations.Story.Rational;

public class RationalLevel4Configuration : NumberConfigurationBase
{
    public const string Name = "4";

    public override string LevelName => Name;

    public override int SecondsPerEquation => 10;

    public override int NumberOfEquations => 1;

    public override NumberOperations[] Operations => new NumberOperations[] { NumberOperations.Multiplication };

    public override int IntervalFrom => 0;

    public override int IntervalTo => 50;
    public override GameType GameType => GameType.Story;
    public override NumberTypes NumberType => NumberTypes.Rational;
    public override string NextLevelName => null;
}
