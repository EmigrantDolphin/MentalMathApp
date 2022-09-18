using MentalMathApp.LevelConfigurations.Enums;

namespace MentalMathApp.LevelConfigurations.Story.Rational;

public class RationalLevel1Configuration : NumberConfigurationBase
{
    public const string Name = "1";

    public override string LevelName => Name;

    public override int SecondsPerEquation => 10;

    public override int NumberOfEquations => 10;

    public override NumberOperations[] Operations => new NumberOperations[] { NumberOperations.Addition };

    public override int IntervalFrom => 0;

    public override int IntervalTo => 10;
    public override GameType GameType => GameType.Story;
    public override string NextLevelName => "2";
    public override NumberTypes NumberType => NumberTypes.Rational;
}
