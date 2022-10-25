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

	public Equation NewEquation(NumberTypes type) => type switch
	{
		NumberTypes.Integer => NewIntegerEquation(),
		NumberTypes.Rational => NewRationalEquation(),
		_ => throw new NotImplementedException($"Tried forming new equation. Number type not implemented: {type}")
	};

	public Equation NewIntegerEquation()
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

		var possibleAnswers = GetPossibleAnswers(answer, randomOperation);
		return new Equation(
			equation,
			randomFirstNumber.ToString(),
			randomSecondNumber.ToString(),
			randomOperation,
			answer.ToString(),
			PossibleAnswer.Hide(possibleAnswers.Select(x => x.ToString()).ToArray()));
	}

	private (int, int) GetRandomNumbers(NumberOperations numberOperator, int from, int to)
	{
		if (numberOperator == NumberOperations.Division)
		{
            var first = _random.Next(_numberConfiguration.IntervalFrom, _numberConfiguration.IntervalTo);
            var second = _random.Next(_numberConfiguration.IntervalFrom, _numberConfiguration.IntervalTo);

			first = first == 0 ? 1 : first; // don't divide by zero

			var product = first * second;

			return (product, first);
		}

		var randomFirstNumber = _random.Next(_numberConfiguration.IntervalFrom, _numberConfiguration.IntervalTo);
		var randomSecondNumber = _random.Next(_numberConfiguration.IntervalFrom, _numberConfiguration.IntervalTo);

		return (randomFirstNumber, randomSecondNumber);
	}

	public Equation NewRationalEquation()
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

		var possibleAnswers = GetPossibleAnswers(answer, randomOperation);
		return new Equation(
			equation,
			randomFirstNumber.ToString(),
			randomSecondNumber.ToString(),
			randomOperation,
			answer.ToString(),
			PossibleAnswer.Hide(possibleAnswers.Select(x => x.ToString()).ToArray()));
	}

	private decimal[] GetPossibleAnswers(decimal realAnswer, NumberOperations operation)
	{
		var possibleAnswers = new List<decimal>();
		for (int i = 0; i < 3; i++)
		{
            var from = _numberConfiguration.IntervalFrom;
            var to = _numberConfiguration.IntervalTo;

            var randomFirstNumber = (decimal)_random.Next(from, to) / to;
            var randomSecondNumber = (decimal)_random.Next(from, to) / to;

            var answer = operation.PerformOperation(randomFirstNumber, randomSecondNumber);

			possibleAnswers.Add(answer);
		}

		// //TODO: get possible answers. My brain is mashed potatoes. Please help. This doesn't work with division.
		// var bits = decimal.GetBits(realAnswer);
		// var bitss = decimal.GetBits(realAnswer)[3];
		// var bytes = BitConverter.GetBytes(bitss);
		// var decimalCount = BitConverter.GetBytes(decimal.GetBits(realAnswer)[3])[2];


		// var differences = GetRandomValues(3);
		// var position = (decimal)-Math.Pow(10, -decimalCount);

		// var deciDifferences = differences.Select(x => x * position).ToList();

		// var answers = deciDifferences.Select(x => x + realAnswer).ToList();
		possibleAnswers.Add(realAnswer);
		possibleAnswers = possibleAnswers.OrderByDescending(x => x).ToList();

		return possibleAnswers.ToArray();
	}

	private int[] GetPossibleAnswers(int realAnswer, NumberOperations operation)
	{
		var possibleAnswers = new List<int>() { realAnswer };

		while (possibleAnswers.Count < 4)
		{
            var (randomFirstNumber, randomSecondNumber) =
                GetRandomNumbers(
                    operation,
                    _numberConfiguration.IntervalFrom,
                    _numberConfiguration.IntervalTo);

            var answer = operation.PerformOperation(randomFirstNumber, randomSecondNumber);
			if (!possibleAnswers.Contains(answer))
			{
                possibleAnswers.Add(answer);
			}
		}

		possibleAnswers = possibleAnswers.OrderByDescending(x => x).ToList();

		return possibleAnswers.ToArray();
	}
}
