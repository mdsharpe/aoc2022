internal class Valley
{
    public Location[,] Map { get; init; }
    public Coordinate Entrance { get; init; }
    public Coordinate Exit { get; init; }
    public int Width => Map.GetLength(0);
    public int Height => Map.GetLength(1);
    public int MinX => 1;
    public int MaxX => Width - 2;
    public int MinY => 1;
    public int MaxY => Height - 2;
    public List<Blizzard> Blizzards { get; } = new List<Blizzard>();
    public List<Expedition> Expeditions { get; } = new List<Expedition>();
    public List<Expedition> ExpeditionsNextGen { get; } = new List<Expedition>();
    public IEnumerable<Actor> Occupants => Enumerable.Concat<Actor>(Blizzards, Expeditions);

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
                        Blizzards.Add(new Blizzard
                        {
                            Coordinate = coordinate,
                            Direction = Direction.Parse(c)
                        });

                        break;

                    case '.':
                        if (!entranceFound && y == 0)
                        {
                            entranceFound = true;
                            Entrance = loc.Coordinate;
                        }

                        if (!exitFound && y == height - 1)
                        {
                            exitFound = true;
                            Exit = loc.Coordinate;
                        }

                        break;

                    default:
                        throw new InvalidOperationException();
                }

                Map[x, y] = loc;
            }
        }
    }

    public void RevExpeditions()
    {
        Expeditions.Clear();
        var nxt = ExpeditionsNextGen.Distinct().ToList();
        Expeditions.AddRange(nxt);
        ExpeditionsNextGen.Clear();
    }

    public bool GetCanOccupy(Coordinate coordinate)
        => (this.Contains(coordinate) && !Blizzards.Any(o => o.Coordinate == coordinate))
        || coordinate == this.Entrance || coordinate == this.Exit;

    public bool Contains(Coordinate coordinate)
        => coordinate.X > 0
        && coordinate.X < Width - 1
        && coordinate.Y > 0
        && coordinate.Y < Height - 1;
}
