internal class Blizzard : Actor
{
    public required Direction Direction { get; init; }

    public override char ToChar() => Direction.Char;

    public override void MoveWithin(Valley valley)
    {
        var dest = this.Coordinate.Add(this.Direction);

        if (dest.X > valley.Width - 2)
        {
            dest = dest.WithX(1);
        }
        else if (dest.X < 1)
        {
            dest = dest.WithX(valley.Width - 2);
        }

        if (dest.Y > valley.Height - 2)
        {
            dest = dest.WithY(1);
        }
        else if (dest.Y < 1)
        {
            dest = dest.WithY(valley.Height - 2);
        }

        Coordinate = dest;
    }
}
