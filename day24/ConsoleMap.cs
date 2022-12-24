using static System.Console;
internal static class ConsoleMap
{
    public static void WriteMap(Valley valley)
    {
        Clear();

        for (var y = 0; y < valley.Height; y++)
        {
            for (var x = 0; x < valley.Width; x++)
            {
                var l = valley.Map[x, y];
                var occupants = valley.Occupants
                    .Where(o => o.Coordinate.X == x && o.Coordinate.Y == y)
                    .ToList();

                char o;

                if (occupants.Any())
                {
                    if (occupants.Count == 1)
                    {
                        o = occupants.Single().ToChar();
                    }
                    else if (occupants.Count < 10)
                    {
                        o = occupants.Count.ToString().Single();
                    }
                    else
                    {
                        o = '*';
                    }
                }
                else if (l.IsWall)
                {
                    o = '#';
                }
                else
                {
                    o = '.';
                }

                Write(o);
            }

            WriteLine();
        }
    }
}
