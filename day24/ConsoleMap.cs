using System.Text;

internal static class ConsoleMap
{
    public static void WriteMap(Valley valley, int minute)
    {
        var output = new StringBuilder();

        for (var y = 0; y < valley.Height; y++)
        {
            for (var x = 0; x < valley.Width; x++)
            {
                var l = valley.Map[x, y];
                var occupants = valley.GetOccupants(new Coordinate { X = x, Y = y });
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

                output.Append(o);
            }

            output.Append(Environment.NewLine);
        }

        try
        {
            Console.Clear();
        }
        catch { }

        Console.Write(output);
        Console.WriteLine();
        Console.WriteLine($"{valley.Expeditions.Count} expeditions.");
        Console.WriteLine($"{minute} minutes passed.");
    }
}
