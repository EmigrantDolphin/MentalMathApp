using MentalMathApp.LevelConfigurations.Enums;

namespace MentalMathApp.Services.EquationFormers.Models;

public record Equation<T>(
    string Formula,
    T FirstValue,
    T SecondValue,
    NumberOperations Operation,
    T Answer,
    T[] PossibleAnswers)
{
    public Equation<string> ToStringEquation()
    {
        return new Equation<string>(
            Formula,
            FirstValue.ToString(),
            SecondValue.ToString(),
            Operation,
            Answer.ToString(),
            PossibleAnswers.Select(x => x.ToString()).ToArray());
    }
}
