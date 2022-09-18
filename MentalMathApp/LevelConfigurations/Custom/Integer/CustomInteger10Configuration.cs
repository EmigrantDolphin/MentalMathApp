using MentalMathApp.LevelConfigurations.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentalMathApp.LevelConfigurations.Custom.Integer;

public class CustomInteger10Configuration : NumberConfigurationBase
{
    public override string LevelName => "10";

    public override int SecondsPerEquation => 10;

    public override int NumberOfEquations => 10;

    public override NumberOperations[] Operations => new NumberOperations[] { NumberOperations.Addition };

    public override int IntervalFrom => 0;

    public override int IntervalTo => 10;

    public override GameType GameType => GameType.Custom;

    public override NumberTypes NumberType => NumberTypes.Integer;

    public override string NextLevelName => null;
}
