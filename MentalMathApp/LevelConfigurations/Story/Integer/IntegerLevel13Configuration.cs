using MentalMathApp.LevelConfigurations.Enums;

namespace MentalMathApp.LevelConfigurations.Story.Integer;

public class IntegerLevel13Configuration : NumberConfigurationBase
{
    public const string Name = "13";

    public override string LevelName => Name;

    public override int SecondsPerEquation => 10;

    public override int NumberOfEquations => 20;

    public override NumberOperations[] Operations =>
        new NumberOperations[] { NumberOperations.Addition, NumberOperations.Subtraction, NumberOperations.Multiplication, NumberOperations.Division };

    public override int IntervalFrom => 0;

    public override int IntervalTo => 10;
    public override GameType GameType => GameType.Story;
    public override string NextLevelName => "14";
    public override NumberTypes NumberType => NumberTypes.Integer;
}
