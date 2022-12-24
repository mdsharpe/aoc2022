internal class Valley
{
    public Location[,] Map { get; init; }
    public int Width => Map.GetLength(0);
    public int Height => Map.GetLength(1);
    public int MinX => 1;
    public int MaxX => Width - 2;
    public int MinY => 1;
    public int MaxY => Height - 2;
    public List<Actor> Occupants { get; } = new List<Actor>();

    public Valley(string[] input)
    {
        var width = input.Max(o => o.Length);
        var height = input.Length;

        Map = new Location[width, height];
        var entranceFound = false;
        var exitFound = false;

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                var c = input[y][x];
                var coordinate = new Coordinate
                {
                    X = x,
                    Y = y
                };
                var loc = new Location { Coordinate = coordinate };

                switch (c)
                {
                    case '#':
                        loc.IsWall = true;
                        break;

                    case '^':
                    case 'v':
                    case '<':
                    case '>':
                        Occupants.Add(new Blizzard
                        {
                            Coordinate = coordinate,
                            Direction = Direction.Parse(c)
                        });

                        break;

                    case '.':
                        if (!entranceFound && y == 0)
                        {
                            loc.IsEntrance = true;
                            entranceFound = true;
                        }
                        if (!exitFound && y == height - 1) {
                            loc.IsExit = true;
                            exitFound = true;
                        }
                        break;

                    default:
                        throw new InvalidOperationException();
                }

                Map[x, y] = loc;
            }
        }
    }

    public Coordinate GetEntrance()
        => Map
            .Cast<Location>()
            .Single(o => o.IsEntrance)
            .Coordinate;

    public Coordinate GetExit()
        => Map
            .Cast<Location>()
            .Single(o => o.IsExit)
            .Coordinate;

    public bool GetCanOccupy(Coordinate coordinate)
        => this.Contains(coordinate)
        && !Occupants.Any(o => o.Coordinate.Equals(coordinate));

    public bool Contains(Coordinate coordinate)
        => coordinate.X > 0
        && coordinate.X < Width - 1
        && coordinate.Y > 0
        && coordinate.Y < Height - 1;
}
