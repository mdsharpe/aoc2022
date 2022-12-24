internal class Blizzard : Actor
{
    public required Direction Direction { get; init; }

    public override char ToChar() => Direction.Char;

    public override void MoveWithin(Valley valley)
    {
        var dest = this.Coordinate.Add(this.Direction);

        if (dest.X > valley.MaxX)
        {
            dest = dest.WithX(valley.MinX);
        }
        else if (dest.X < valley.MinX)
        {
            dest = dest.WithX(valley.MaxX);
        }

        if (dest.Y > valley.MaxY)
        {
            dest = dest.WithY(valley.MinY);
        }
        else if (dest.Y < valley.MinY)
        {
            dest = dest.WithY(valley.MaxY);
        }

        Coordinate = dest;
    }
}
