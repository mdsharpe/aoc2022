internal struct Coordinate
{
    public required int X { get; init; }
    public required int Y { get; init; }

    public static bool operator ==(Coordinate a, Coordinate b) => a.Equals(b);
    public static bool operator !=(Coordinate a, Coordinate b) => !(a == b);

    public Coordinate Add(Direction direction)
        => new Coordinate
        {
            X = this.X + direction.Dx,
            Y = this.Y + direction.Dy
        };

    public Coordinate WithX(int x)
        => new Coordinate
        {
            X = x,
            Y = this.Y
        };

    public Coordinate WithY(int y)
        => new Coordinate
        {
            X = this.X,
            Y = y
        };

    public override bool Equals(object? obj)
        => obj is Coordinate other
        && X == other.X
        && Y == other.Y;

    public override int GetHashCode() => HashCode.Combine(X, Y);
}
