using System.Text.RegularExpressions;

class Move
{
    private static readonly Regex ParseRegex = new Regex("move (\\d+) from (\\d+) to (\\d+)");

    public int Quantity { get; private init; }
    public int From { get; private init; }
    public int To { get; private init; }

    public static Move? Parse(string input)
    {
        var match = ParseRegex.Match(input);

        if (!match.Success)
        {
            return null;
        }

        return new Move
        {
            Quantity = int.Parse(match.Groups[1].Value),
            From = int.Parse(match.Groups[2].Value),
            To = int.Parse(match.Groups[3].Value)
        };
    }
}