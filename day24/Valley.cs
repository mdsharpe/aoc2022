internal class Valley
{
    private Dictionary<Coordinate, List<Actor>> _occupants = new Dictionary<Coordinate, List<Actor>>();
    private HashSet<Coordinate> _blizzardCoordinates = new HashSet<Coordinate>();

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

        Expeditions.Add(new Expedition { Coordinate = this.Entrance });

        HashBlizzardCoordinates();
        BuildOccupantsDictionary();
    }

    public List<Actor> GetOccupants(Coordinate coordinate)
    {
        if (_occupants.TryGetValue(coordinate, out var o))
        {
            return o;
        }

        return new List<Actor>();
    }

    public void Tick()
    {
        Parallel.ForEach(Blizzards, o => o.MoveWithin(this));
        HashBlizzardCoordinates();

        foreach (var actor in Expeditions)
        {
            actor.MoveWithin(this);
        }

        Expeditions.Clear();
        Expeditions.AddRange(ExpeditionsNextGen.Distinct());
        ExpeditionsNextGen.Clear();

        BuildOccupantsDictionary();
    }

    public bool GetCanOccupy(Coordinate coordinate)
    {
        if (coordinate == this.Entrance || coordinate == this.Exit)
        {
            return true;
        }

        if (!this.Contains(coordinate))
        {
            return false;
        }

        return !_blizzardCoordinates.Contains(coordinate);
    }

    public bool Contains(Coordinate coordinate)
        => coordinate.X > 0
        && coordinate.X < Width - 1
        && coordinate.Y > 0
        && coordinate.Y < Height - 1;

    private void BuildOccupantsDictionary()
    {
        _occupants = Enumerable.Concat<Actor>(Blizzards, Expeditions)
            .GroupBy(o => o.Coordinate)
            .ToDictionary(o => o.Key, o => o.ToList());
    }

    private void HashBlizzardCoordinates()
    {
        _blizzardCoordinates = Blizzards.Select(o => o.Coordinate).ToHashSet();
    }
}
