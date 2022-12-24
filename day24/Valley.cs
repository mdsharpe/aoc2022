internal class Valley
{
    public Location[,] Map { get; init; }
    public int Width => Map.GetLength(0);
    public int Height => Map.GetLength(1);
    public List<Actor> Occupants { get; } = new List<Actor>();

    public Valley(string[] input)
    {
        var width = input.Max(o => o.Length);
        var height = input.Length;

        Map = new Location[width, height];

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                var c = input[y][x];
                var loc = new Location();

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
                            Coordinate = new Coordinate
                            {
                                X = x,
                                Y = y
                            },
                            Direction = Direction.Parse(c)
                        });

                        break;

                    default:
                        break;
                }

                Map[x, y] = loc;
            }
        }
    }
}
