using MentalMathApp.LevelConfigurations.Enums;

namespace MentalMathApp.LevelConfigurations.Story.Integer;

public class RationalLevel13Configuration : NumberConfigurationBase
{
    public const string Name = "13";

    public override string LevelName => Name;

    public override int SecondsPerEquation => 5;

    public override int NumberOfEquations => 25;

    public override NumberOperations[] Operations =>
        new NumberOperations[] { NumberOperations.Addition, NumberOperations.Subtraction, NumberOperations.Multiplication };

    public override int IntervalFrom => 0;

    public override int IntervalTo => 500;
    public override GameType GameType => GameType.Story;
    public override string NextLevelName => "13";
    public override NumberTypes NumberType => NumberTypes.Rational;
}
