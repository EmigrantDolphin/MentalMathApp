using MentalMathApp.LevelConfigurations.Enums;

namespace MentalMathApp.LevelConfigurations.Story.Integer;

public class IntegerLevel10Configuration : NumberConfigurationBase
{
    public const string Name = "10";

    public override string LevelName => Name;

    public override int SecondsPerEquation => 10;

    public override int NumberOfEquations => 20;

    public override NumberOperations[] Operations =>
        new NumberOperations[] { NumberOperations.Addition, NumberOperations.Subtraction, NumberOperations.Multiplication };

    public override int IntervalFrom => 0;

    public override int IntervalTo => 50;
    public override GameType GameType => GameType.Story;
    public override string NextLevelName => "11";
    public override NumberTypes NumberType => NumberTypes.Integer;
}
