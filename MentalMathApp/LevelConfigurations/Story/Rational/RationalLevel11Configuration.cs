using MentalMathApp.LevelConfigurations.Enums;

namespace MentalMathApp.LevelConfigurations.Story.Integer;

public class RationalLevel11Configuration : NumberConfigurationBase
{
    public const string Name = "11";

    public override string LevelName => Name;

    public override int SecondsPerEquation => 10;

    public override int NumberOfEquations => 20;

    public override NumberOperations[] Operations =>
        new NumberOperations[] { NumberOperations.Addition, NumberOperations.Subtraction, NumberOperations.Multiplication };

    public override int IntervalFrom => 0;

    public override int IntervalTo => 100;
    public override GameType GameType => GameType.Story;
    public override string NextLevelName => "12";
    public override NumberTypes NumberType => NumberTypes.Rational;
}
