struct Coords
{
    public static readonly Coords Zero = new Coords { X = 0, Y = 0 };

    public required int X { get; init; }
    public required int Y { get; init; }

    public Coords Move(Move move) => new Coords { X = X + move.DX, Y = Y + move.DY };

    public Coords MoveTowards(Coords target)
    {
        int x = X, y = Y;

        if (target.X > X)
        {
            x++;
        }
        else if (target.X < X)
        {
            x--;
        }

        if (target.Y > Y)
        {
            y++;
        }
        else if (target.Y < Y)
        {
            y--;
        }

        return new Coords { X = x, Y = y };
    }

    public bool GetIsAdjacentTo(Coords other)
        => Math.Abs(other.X - X) <= 1 && Math.Abs(other.Y - Y) <= 1;
}
