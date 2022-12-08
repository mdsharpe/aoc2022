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

        (bool Visible, int ViewingDistance) InspectDirection(Func<KeyValuePair<(int X, int Y), Tree>, bool> directionClause)
        {
            var neighbours = trees.Where(directionClause).ToList();
            var blockers = neighbours.Where(t => t.Value.Height >= tree.Height).ToList();
            var visible = !blockers.Any();

            int viewingDistance;
            if (visible)
            {
                viewingDistance = neighbours.Count;
            }
            else
            {
                viewingDistance = (from t in blockers
                                   let distX = Math.Abs(x - t.Key.X)
                                   let distY = Math.Abs(y - t.Key.Y)
                                   select distX + distY).Min();
            }

            return (visible, viewingDistance);
        }

        var left = InspectDirection(t => t.Key.Y == y && t.Key.X < x);
        var right = InspectDirection(t => t.Key.Y == y && t.Key.X > x);
        var up = InspectDirection(t => t.Key.Y < y && t.Key.X == x);
        var down = InspectDirection(t => t.Key.Y > y && t.Key.X == x);

        tree.VisibleFromOutside = left.Visible || right.Visible || up.Visible || down.Visible;
        tree.ScenicScore = left.ViewingDistance * right.ViewingDistance * up.ViewingDistance * down.ViewingDistance;
    }
}

Console.WriteLine($"Visible trees: {trees.Values.Count(o => o.VisibleFromOutside)}");
Console.WriteLine($"Max scenic score: {trees.Values.Max(o => o.ScenicScore)}");
