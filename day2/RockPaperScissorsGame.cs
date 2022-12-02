class RockPaperScissorsGame
{
    public RockPaperScissors OpponentSelection { get; init; }
    public RockPaperScissors YourSelection { get; init; }

    public ResultType Result
    {
        get
        {
            if (YourSelection == OpponentSelection)
            {
                return ResultType.Draw;
            }

            var yourWin = OpponentSelection switch
            {
                RockPaperScissors.Rock => YourSelection == RockPaperScissors.Paper,
                RockPaperScissors.Paper => YourSelection == RockPaperScissors.Scissors,
                RockPaperScissors.Scissors => YourSelection == RockPaperScissors.Rock,
                _ => throw new InvalidOperationException()
            };

            return yourWin ? ResultType.YourWin : ResultType.OpponentWin;
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

            var outcomeScore = Result switch
            {
                ResultType.OpponentWin => 0,
                ResultType.Draw => 3,
                ResultType.YourWin => 6,
                _ => throw new InvalidOperationException()
            };

            return shapeScore + outcomeScore;
        }
    }

    public static RockPaperScissorsGame Parse(string input)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(input);

        var selectionStrings = input.Split(' ');

        if (selectionStrings.Length != 2 || !selectionStrings.All(o => o.Length == 1))
        {
            throw new ArgumentOutOfRangeException();
        }

        return new RockPaperScissorsGame
        {
            OpponentSelection = selectionStrings[0] switch
            {
                "A" => RockPaperScissors.Rock,
                "B" => RockPaperScissors.Paper,
                "C" => RockPaperScissors.Scissors,
                _ => throw new ArgumentOutOfRangeException()
            },
            YourSelection = selectionStrings[1] switch
            {
                "X" => RockPaperScissors.Rock,
                "Y" => RockPaperScissors.Paper,
                "Z" => RockPaperScissors.Scissors,
                _ => throw new ArgumentOutOfRangeException()
            },
        };
    }
}
