var input = await System.IO.File.ReadAllLinesAsync(args[0]);
var width = input.Select(o => o.Length).Max();
var height = input.Length;

var trees = new Dictionary<(int X, int Y), Tree>();

for (var y = 0; y < height; y++)
{
    for (var x = 0; x < width; x++)
    {
        trees.Add((x, y), new Tree { Height = int.Parse(input[y][x].ToString()) });
    }
}

var visibleCount = 0;

for (var y = 0; y < height; y++)
{
    for (var x = 0; x < width; x++)
    {
        var tree = trees[(x, y)];

        bool GetBlocked(Func<KeyValuePair<(int X, int Y), Tree>, bool> directionCheck)
            => trees.Where(directionCheck).Any(t => t.Value.Height >= tree.Height);

        if (!GetBlocked(t => t.Key.Y == y && t.Key.X < x)
            || !GetBlocked(t => t.Key.Y == y && t.Key.X > x)
            || !GetBlocked(t => t.Key.Y < y && t.Key.X == x)
            || !GetBlocked(t => t.Key.Y > y && t.Key.X == x))
        {
            visibleCount++;
        }
    }
}

Console.WriteLine($"Visible trees: {visibleCount}");
