using System.Linq;

var input = await System.IO.File.ReadAllLinesAsync(args[0]);

var width = input.Select(o => o.Length).Max();
var height = input.Length;

var trees = new Dictionary<(int X, int Y), int>();

for (var y = 0; y < height; y++)
{
    for (var x = 0; x < width; x++)
    {
        trees.Add((x, y), int.Parse(input[y][x].ToString()));
    }
}

var visibleCount = 0;

for (var y = 0; y < height; y++)
{
    for (var x = 0; x < width; x++)
    {
        var tree = trees[(x, y)];

        var blockedLeft = trees.Any(t => t.Key.Y == y && t.Key.X < x && t.Value >= tree);
        var blockedRight = trees.Any(t => t.Key.Y == y && t.Key.X > x && t.Value >= tree);
        var blockedUp = trees.Any(t => t.Key.Y < y && t.Key.X == x && t.Value >= tree);
        var blockedDown = trees.Any(t => t.Key.Y > y && t.Key.X == x && t.Value >= tree);

        if (!blockedLeft || !blockedRight || !blockedUp || !blockedDown)
        {
            visibleCount++;
        }
    }
}

Console.WriteLine($"Visible trees: {visibleCount}");

