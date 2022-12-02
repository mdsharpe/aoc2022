class RockPaperScissorsGame
{
    public RockPaperScissors OpponentSelection { get; init; }
    public RockPaperScissors YourSelection { get; private set; }

    public ResultType Outcome
    {
        get
        {
            if (YourSelection == OpponentSelection)
            {
                return ResultType.Draw;
            }

            var yourWin = YourSelection == GetWinningSelection(OpponentSelection);

            return yourWin ? ResultType.YourWin : ResultType.OpponentWin;
        }

        set
        {
            YourSelection = value switch
            {
                ResultType.OpponentWin => GetLosingSelection(OpponentSelection),
                ResultType.Draw => OpponentSelection,
                ResultType.YourWin => GetWinningSelection(OpponentSelection),
                _ => throw new InvalidOperationException()
            };
        }
    }

    public int Score
    {
        get
        {
            var shapeScore = YourSelection switch
            {
                RockPaperScissors.Rock => 1,
                RockPaperScissors.Paper => 2,
                RockPaperScissors.Scissors => 3,
                _ => throw new InvalidOperationException()
            };

            var outcomeScore = Outcome switch
            {
                ResultType.OpponentWin => 0,
                ResultType.Draw => 3,
                ResultType.YourWin => 6,
                _ => throw new InvalidOperationException()
            };

            return shapeScore + outcomeScore;
        }
    }

    public static RockPaperScissorsGame Parse(string input, bool secondColIsOutcome)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(input);

        var selectionStrings = input.Split(' ');

        if (selectionStrings.Length != 2 || !selectionStrings.All(o => o.Length == 1))
        {
            throw new ArgumentOutOfRangeException();
        }

        var game = new RockPaperScissorsGame
        {
            OpponentSelection = selectionStrings[0] switch
            {
                "A" => RockPaperScissors.Rock,
                "B" => RockPaperScissors.Paper,
                "C" => RockPaperScissors.Scissors,
                _ => throw new ArgumentOutOfRangeException()
            }
        };

        if (secondColIsOutcome)
        {
            game.Outcome = selectionStrings[1] switch
            {
                "X" => ResultType.OpponentWin,
                "Y" => ResultType.Draw,
                "Z" => ResultType.YourWin,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        else
        {
            game.YourSelection = selectionStrings[1] switch
            {
                "X" => RockPaperScissors.Rock,
                "Y" => RockPaperScissors.Paper,
                "Z" => RockPaperScissors.Scissors,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        return game;
    }

    private static RockPaperScissors GetWinningSelection(RockPaperScissors value) => value switch
    {
        RockPaperScissors.Rock => RockPaperScissors.Paper,
        RockPaperScissors.Paper => RockPaperScissors.Scissors,
        RockPaperScissors.Scissors => RockPaperScissors.Rock,
        _ => throw new ArgumentOutOfRangeException()
    };

    private static RockPaperScissors GetLosingSelection(RockPaperScissors value) => value switch
    {
        RockPaperScissors.Rock => RockPaperScissors.Scissors,
        RockPaperScissors.Paper => RockPaperScissors.Rock,
        RockPaperScissors.Scissors => RockPaperScissors.Paper,
        _ => throw new ArgumentOutOfRangeException()
    };
}
