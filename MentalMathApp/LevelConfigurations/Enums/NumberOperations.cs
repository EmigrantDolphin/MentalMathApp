namespace MentalMathApp.LevelConfigurations.Enums;

public enum NumberOperations
{
    Addition,
    Subtraction,
    Multiplication,
    Division
}

public static class NumberOperationsExtensions
{
    public static string GetMathematicalSymbol(this NumberOperations operation) => operation switch
    {
        NumberOperations.Addition => "+",
        NumberOperations.Subtraction => "-",
        NumberOperations.Division => "/",
        NumberOperations.Multiplication => "*",
        _ => throw new Exception("Sumimasen")
    };

    public static int PerformOperation(this NumberOperations operation, int firstNumber, int secondNumber) => operation switch
    {
        NumberOperations.Addition => firstNumber + secondNumber,
        NumberOperations.Subtraction => firstNumber - secondNumber,
        NumberOperations.Division => firstNumber / secondNumber,
        NumberOperations.Multiplication => firstNumber * secondNumber,
        _ => throw new Exception("Sumimasen")
    };

    public static decimal PerformOperation(this NumberOperations operation, decimal firstNumber, decimal secondNumber) => operation switch
    {
        NumberOperations.Addition => firstNumber + secondNumber,
        NumberOperations.Subtraction => firstNumber - secondNumber,
        NumberOperations.Division => firstNumber / secondNumber,
        NumberOperations.Multiplication => firstNumber * secondNumber,
        _ => throw new Exception("Sumimasen")
    };

    public static string ToHistoryString(this NumberOperations[] operations)
    {
        var mathematicalRepresentations = operations.Select(x => x.GetMathematicalSymbol()).ToList();

        return string.Join(",", mathematicalRepresentations);
    }
}
