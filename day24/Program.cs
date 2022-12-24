var input = await System.IO.File.ReadAllLinesAsync(args[0]);
var valley = new Valley(input.Max(o => o.Length), input.Length);

for (var y = 0; y < valley.Height; y++)
{
    for (var x = 0; x < valley.Width; x++)
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
                valley.Occupants.Add(new Blizzard
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

        valley.Map[x, y] = loc;
    }
}

ConsoleMap.WriteMap(valley);
await Task.Delay(1000);

do
{
    foreach (var actor in valley.Occupants)
    {
        actor.MoveWithin(valley);
    }

    ConsoleMap.WriteMap(valley);
    await Task.Delay(1000);
} while (true);
