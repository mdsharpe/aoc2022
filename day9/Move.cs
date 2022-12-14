using System.Text.RegularExpressions;

struct Move
{
    private static readonly Regex ParseRegex = new Regex("([UDLR]) (\\d+)");

    private static readonly Move Up = new Move { DX = 0, DY = -1 };
    private static readonly Move Down = new Move { DX = 0, DY = 1 };
    private static readonly Move Left = new Move { DX = -1, DY = 0 };
    private static readonly Move Right = new Move { DX = 1, DY = 0 };

    public required int DX { get; init; }
    public required int DY { get; init; }

    public static IEnumerable<Move> ParseMany(string input)
    {
        var match = ParseRegex.Match(input);

        if (!match.Success)
        {
            throw new InvalidOperationException();
        }

        var dir = match.Groups[1].Value.Single();
        var count = int.Parse(match.Groups[2].Value);

        var move = dir switch
        {
            'U' => Up,
            'D' => Down,
            'L' => Left,
            'R' => Right,
            _ => throw new InvalidOperationException()
        };

        return Enumerable.Repeat(move, count);
    }
}
