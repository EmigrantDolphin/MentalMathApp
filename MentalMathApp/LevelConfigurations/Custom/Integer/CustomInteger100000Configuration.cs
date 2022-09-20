using MentalMathApp.LevelConfigurations.Enums;

namespace MentalMathApp.LevelConfigurations.Custom.Integer;

public class CustomInteger100000Configuration : NumberConfigurationBase
{
    public override string LevelName => "100000";

    public override int SecondsPerEquation => 10;

    public override int NumberOfEquations => 10;

    public override NumberOperations[] Operations => new NumberOperations[] { NumberOperations.Addition };

    public override int IntervalFrom => 0;

    public override int IntervalTo => 100000;

    public override GameType GameType => GameType.Custom;

    public override NumberTypes NumberType => NumberTypes.Integer;

    public override string NextLevelName => null;
}
