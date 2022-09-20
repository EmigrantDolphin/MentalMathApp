using MentalMathApp.LevelConfigurations;
using MentalMathApp.LevelConfigurations.Enums;
using MentalMathApp.Services.EquationFormers.Models;

namespace MentalMathApp.Services.EquationFormers;

public class NumberEquationFormer
{
	private readonly NumberConfigurationBase _numberConfiguration;
	private readonly Random _random = new();

	public NumberEquationFormer(NumberConfigurationBase numberConfiguration)
	{
		_numberConfiguration = numberConfiguration;
		NumberOfEquations = _numberConfiguration.NumberOfEquations;
	}

	public int EquationsGenerated { get; private set; }
	public int NumberOfEquations { get; private set; }

	public Equation<string> NewEquation(NumberTypes type) => type switch
	{
		NumberTypes.Integer => NewIntegerEquation().ToStringEquation(),
		NumberTypes.Rational => NewRationalEquation().ToStringEquation(),
		_ => throw new NotImplementedException($"Tried forming new equation. Number type not implemented: {type}")
	};

	public Equation<int> NewIntegerEquation()
	{
		EquationsGenerated++;

		var randomOperationIndex = _random.Next(0, _numberConfiguration.Operations.Count());
		var randomOperation = _numberConfiguration.Operations[randomOperationIndex];

		var (randomFirstNumber, randomSecondNumber) =
			GetRandomNumbers(
				randomOperation,
				_numberConfiguration.IntervalFrom,
				_numberConfiguration.IntervalTo);

		var equation = $"{randomFirstNumber} {randomOperation.GetMathematicalSymbol()} {randomSecondNumber}";

		var answer = randomOperation.PerformOperation(randomFirstNumber, randomSecondNumber);

		var possibleAnswers = GetPossibleAnswers(answer);
		return new Equation<int>(equation, randomFirstNumber, randomSecondNumber, randomOperation, answer, possibleAnswers);
	}

	private (int, int) GetRandomNumbers(NumberOperations numberOperator, int from, int to)
	{
		if (numberOperator == NumberOperations.Division)
		{
            var first = _random.Next(_numberConfiguration.IntervalFrom, _numberConfiguration.IntervalTo);
            var second = _random.Next(_numberConfiguration.IntervalFrom, _numberConfiguration.IntervalTo);

			var product = first * second;

			return (product, first);
		}

		var randomFirstNumber = _random.Next(_numberConfiguration.IntervalFrom, _numberConfiguration.IntervalTo);
		var randomSecondNumber = _random.Next(_numberConfiguration.IntervalFrom, _numberConfiguration.IntervalTo);

		return (randomFirstNumber, randomSecondNumber);
	}

	public Equation<decimal> NewRationalEquation()
	{
		EquationsGenerated++;

		var from = _numberConfiguration.IntervalFrom;
		var to = _numberConfiguration.IntervalTo;

        var randomFirstNumber = (decimal)_random.Next(from, to) / to;
		var randomSecondNumber = (decimal)_random.Next(from, to) / to;

		var randomOperationIndex = _random.Next(0, _numberConfiguration.Operations.Length);
		var randomOperation = _numberConfiguration.Operations[randomOperationIndex];

		var equation = $"{randomFirstNumber} {randomOperation.GetMathematicalSymbol()} {randomSecondNumber}";

		var answer = randomOperation.PerformOperation(randomFirstNumber, randomSecondNumber);

		var possibleAnswers = GetPossibleAnswers(answer);
		return new Equation<decimal>(equation, randomFirstNumber, randomSecondNumber, randomOperation, answer, possibleAnswers);
	}

	private decimal[] GetPossibleAnswers(decimal realAnswer)
	{
		//TODO: get possible answers. My brain is mashed potatoes. Please help. This doesn't work with division.
		var bits = decimal.GetBits(realAnswer);
		var bitss = decimal.GetBits(realAnswer)[3];
		var bytes = BitConverter.GetBytes(bitss);
		var decimalCount = BitConverter.GetBytes(decimal.GetBits(realAnswer)[3])[2];


		var differences = GetRandomValues(3);
		var position = (decimal)-Math.Pow(10, -decimalCount);

		var deciDifferences = differences.Select(x => x * position).ToList();

		var answers = deciDifferences.Select(x => x + realAnswer).ToList();
		answers.Add(realAnswer);
		answers = answers.OrderByDescending(x => x).ToList();

		return answers.ToArray();
	}

	private int[] GetPossibleAnswers(int realAnswer)
	{
		var differences = GetRandomValues(3);

		var possibleAnswers = differences.Select(x => x + realAnswer).ToList();
		possibleAnswers.Add(realAnswer);
		possibleAnswers = possibleAnswers.OrderByDescending(x => x).ToList();

		return possibleAnswers.ToArray();
	}

	private int[] GetRandomValues(int count)
	{
		var differenceList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

		int[] differences = new int[count];
		for (int i = 0; i < count; i++)
		{
			var differenceIndex = _random.Next(0, differenceList.Count);
			differences[i] = differenceList[differenceIndex];

			differenceList.RemoveAt(differenceIndex);
		}

		for (int i = 0; i < count; i++)
		{
			if (_random.Next(0,2) == 0)
			{
				differences[i] = -differences[i];
			}
		}

		return differences;
	}
}
