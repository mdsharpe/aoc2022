var input = await System.IO.File.ReadAllLinesAsync(args[0]);
var knotCount = int.Parse(args[1]);
var moves = input.SelectMany(line => Move.ParseMany(line)).ToArray();

var knots = Enumerable.Repeat(Coords.Zero, knotCount).ToArray();
var tailVisited = new HashSet<Coords>();

foreach (var move in moves)
{
    knots[0] = knots[0].Move(move);

    for (var i = 1; i < knots.Length; i++)
    {
        if (!knots[i].GetIsAdjacentTo(knots[i - 1]))
        {
            knots[i] = knots[i].MoveTowards(knots[i - 1]);
        }
    }

    tailVisited.Add(knots.Last());
}

Console.WriteLine($"Tail visited {tailVisited.Count} distinct locations");
