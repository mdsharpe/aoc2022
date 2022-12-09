var input = await System.IO.File.ReadAllLinesAsync(args[0]);
var moveSets = input.Select(line => Move.Parse(line)).ToArray();

var head = new Coords { X = 0, Y = 0 };
var tail = new Coords { X = 0, Y = 0 };
var tailVisited = new HashSet<Coords>();

foreach (var moveSet in moveSets)
{
    foreach (var move in Enumerable.Repeat(moveSet.Move, moveSet.Count))
    {
        head = head.Move(move);

        if (!tail.GetIsAdjacentTo(head))
        {
            tail = tail.MoveTowards(head);
        }

        tailVisited.Add(tail);
    }
}

Console.WriteLine($"Tail visited {tailVisited.Count} distinct locations");
