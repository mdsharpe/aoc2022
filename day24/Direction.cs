internal struct Direction
{
    public static Direction Up = new Direction { Dy = -1, Char = '^' };
    public static Direction Down = new Direction { Dy = 1, Char = 'v' };
    public static Direction Left = new Direction { Dx = -1, Char = '<' };
    public static Direction Right = new Direction { Dx = 1, Char = '>' };

    public int Dx { get; init; }
    public int Dy { get; init; }
    public char Char { get; init; }

    public static Direction Parse(char c)
        => EnumeratePossibleDirections().FirstOrDefault(o => o.Char == c);

    private static IEnumerable<Direction> EnumeratePossibleDirections()
    {
        yield return Up;
        yield return Down;
        yield return Left;
        yield return Right;
    }
}
