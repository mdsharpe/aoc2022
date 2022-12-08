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

for (var y = 0; y < height; y++)
{
    for (var x = 0; x < width; x++)
    {
        var tree = trees[(x, y)];

        (bool Visible, int ScenicScore) InspectDirection(Func<KeyValuePair<(int X, int Y), Tree>, bool> directionClause)
        {
            var visible = trees.Where(directionClause).All(t => t.Value.Height < tree.Height);

            return (visible, 0);
        }

        var left = InspectDirection(t => t.Key.Y == y && t.Key.X < x);
        var right = InspectDirection(t => t.Key.Y == y && t.Key.X > x);
        var up = InspectDirection(t => t.Key.Y < y && t.Key.X == x);
        var down = InspectDirection(t => t.Key.Y > y && t.Key.X == x);

        tree.VisibleFromOutside = left.Visible || right.Visible || up.Visible || down.Visible;
        tree.ScenicScore = left.ScenicScore * right.ScenicScore * up.ScenicScore * down.ScenicScore;
    }
}

Console.WriteLine($"Visible trees: {trees.Values.Count(o => o.VisibleFromOutside)}");
Console.WriteLine($"Max scenic score: {trees.Values.Max(o => o.ScenicScore)}");
