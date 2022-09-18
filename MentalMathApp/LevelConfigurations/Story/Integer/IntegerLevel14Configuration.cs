using MentalMathApp.LevelConfigurations.Enums;

namespace MentalMathApp.LevelConfigurations.Story.Integer;

public class IntegerLevel14Configuration : NumberConfigurationBase
{
    public const string Name = "14";

    public override string LevelName => Name;

    public override int SecondsPerEquation => 10;

    public override int NumberOfEquations => 20;

    public override NumberOperations[] Operations =>
        new NumberOperations[] { NumberOperations.Addition, NumberOperations.Subtraction, NumberOperations.Multiplication, NumberOperations.Division };

    public override int IntervalFrom => 0;

    public override int IntervalTo => 30;
    public override GameType GameType => GameType.Story;
    public override string NextLevelName => "15";
    public override NumberTypes NumberType => NumberTypes.Integer;
}
