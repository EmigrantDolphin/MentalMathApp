using MentalMathApp.LevelConfigurations.Enums;

namespace MentalMathApp.LevelConfigurations.Story.Integer;

public class IntegerLevel17Configuration : NumberConfigurationBase
{
    public const string Name = "17";

    public override string LevelName => Name;

    public override int SecondsPerEquation => 5;

    public override int NumberOfEquations => 30;

    public override NumberOperations[] Operations =>
        new NumberOperations[] { NumberOperations.Addition, NumberOperations.Subtraction, NumberOperations.Multiplication, NumberOperations.Division };

    public override int IntervalFrom => 0;

    public override int IntervalTo => 100;
    public override GameType GameType => GameType.Story;
    public override string NextLevelName => null;
    public override NumberTypes NumberType => NumberTypes.Integer;
}
