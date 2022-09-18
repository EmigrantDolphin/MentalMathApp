using MentalMathApp.LevelConfigurations.Enums;

namespace MentalMathApp.LevelConfigurations.Story.Integer;

public class RationalLevel14Configuration : NumberConfigurationBase
{
    public const string Name = "14";

    public override string LevelName => Name;

    public override int SecondsPerEquation => 4;

    public override int NumberOfEquations => 30;

    public override NumberOperations[] Operations =>
        new NumberOperations[] { NumberOperations.Addition, NumberOperations.Subtraction, NumberOperations.Multiplication };

    public override int IntervalFrom => 0;

    public override int IntervalTo => 1000;
    public override GameType GameType => GameType.Story;
    public override string NextLevelName => null;
    public override NumberTypes NumberType => NumberTypes.Rational;
}
