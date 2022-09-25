using MentalMathApp.LevelConfigurations.Enums;

namespace MentalMathApp.Services.EquationFormers.Models;

public record PossibleAnswer(string Answer, string HiddenAnswer)
{
    public static readonly char PreappendChar = '*';
    public static readonly char HiddenChar = 'x';
    public static PossibleAnswer[] Hide(string[] answers)
    {
        var possibleAnswers = answers.Select(x => new PossibleAnswer(x, x)).ToList();

        var preAppendedAnswers = PreappendToSameLength(possibleAnswers);
        int positionOfDistinctNumbers = GetPositionOfDistinctColumn(preAppendedAnswers);

        if (positionOfDistinctNumbers == -1)
        {
            return possibleAnswers.ToArray();
        }

        var hiddenAnswers = HideAnswers(preAppendedAnswers, positionOfDistinctNumbers);

        var debug = hiddenAnswers.Select(x => $"{x.Answer} | {x.HiddenAnswer}").ToList();
        return hiddenAnswers.ToArray();
    }

    private static List<PossibleAnswer> PreappendToSameLength(List<PossibleAnswer> possibleAnswers)
    {
        var preAppendedAnswers = new List<PossibleAnswer>();    
        var maxLength = possibleAnswers.Select(x => x.Answer.Length).Max();
        var minLength = possibleAnswers.Select(x => x.Answer.Length).Min();

        foreach(var possibleAnswer in possibleAnswers)
        {
            var preappendCharCount = maxLength - possibleAnswer.Answer.Length <= 0 ?
                0 :
                maxLength - possibleAnswer.Answer.Length;

            var preappendedAnswer = possibleAnswer;
            for (int i = 0; i < preappendCharCount; i++)
            {
                if (!preappendedAnswer.HiddenAnswer[i].Equals('-')) // if not negative, add preappendChar to front
                {
                    preappendedAnswer = preappendedAnswer
                        with { HiddenAnswer = preappendedAnswer.HiddenAnswer.Insert(0, PreappendChar.ToString()) };
                }
                else // add it after negative sign
                {
                    preappendedAnswer = preappendedAnswer
                        with { HiddenAnswer = preappendedAnswer.HiddenAnswer.Insert(1, PreappendChar.ToString()) };
                }
            }

            preAppendedAnswers.Add(preappendedAnswer);
        }

        return preAppendedAnswers;
    }

    private static int GetPositionOfDistinctColumn(List<PossibleAnswer> preAppendedAnswers)
    {
        var maxAnswerLength = preAppendedAnswers.Select(x => x.Answer.Length).Max();
        int positionOfDistinctNumbers = -1;

        for (int i = 0; i < maxAnswerLength; i++)
        {
            var columnNumbers = preAppendedAnswers.Select(x => x.HiddenAnswer[i]).ToList();
            var distinctColumnNumbers = columnNumbers.Distinct().Where(x => !x.Equals(PreappendChar));

            if (distinctColumnNumbers.Count() == preAppendedAnswers.Count())
            {
                positionOfDistinctNumbers = i;
                break;
            }
        }

        return positionOfDistinctNumbers;
    }

    private static List<PossibleAnswer> HideAnswers(List<PossibleAnswer> preAppendedAnswers, int positionOfDistinctNumbers)
    {
        var hiddenAnswers = new List<PossibleAnswer>();

        foreach(var preAppendedAnswer in preAppendedAnswers)
        {
            var answer = preAppendedAnswer.HiddenAnswer;

            var answerChars = answer.ToCharArray();
            for (int i = 0; i < answerChars.Length; i++)
            {
                if (i != positionOfDistinctNumbers && // don't hide distinct column
                    !answerChars[i].Equals(PreappendChar) && //don't hide preappendChar it's deleted later.
                    !answerChars[i].Equals('-') && //don't hide negative sign
                    !answerChars[i].Equals('.')) // don't hide decimal separator
                {
                    answerChars[i] = HiddenChar;
                }
            }
            var hiddenAnswer = new string(answerChars).Replace(PreappendChar.ToString(), string.Empty);
            hiddenAnswers.Add(preAppendedAnswer with { HiddenAnswer = hiddenAnswer });
        }

        return hiddenAnswers;
    }
}

public record Equation(
    string Formula,
    string FirstValue,
    string SecondValue,
    NumberOperations Operation,
    string Answer,
    PossibleAnswer[] PossibleAnswers);
