internal struct Coordinate
{
    public required int X { get; init; }
    public required int Y { get; init; }

    public static bool operator ==(Coordinate lhs, Coordinate rhs)
    {
        return lhs.Equals(rhs);
    }

    public static bool operator !=(Coordinate lhs, Coordinate rhs) => !(lhs == rhs);

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
}
