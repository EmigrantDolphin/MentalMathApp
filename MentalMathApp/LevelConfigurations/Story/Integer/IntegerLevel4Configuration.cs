using MentalMathApp.LevelConfigurations.Enums;

namespace MentalMathApp.LevelConfigurations.Story.Integer;

public class IntegerLevel4Configuration : NumberConfigurationBase
{

    public const string Name = "4";

    public override string LevelName => Name;

    public override int SecondsPerEquation => 10;

    public override int NumberOfEquations => 10;

    public override NumberOperations[] Operations => new NumberOperations[] { NumberOperations.Subtraction };

    public override int IntervalFrom => 0;

    public override int IntervalTo => 50;
    public override GameType GameType => GameType.Story;
    public override string NextLevelName => "5";
    public override NumberTypes NumberType => NumberTypes.Integer;
}
