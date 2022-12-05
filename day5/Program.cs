var input = await System.IO.File.ReadAllLinesAsync(args[0]);
var part2Mode = args.Length > 1;

var stacks = StackParser.Parse(input);

var moves = input
    .Select(o => Move.Parse(o))
    .Where(o => o != null)
    .Select(o => o!)
    .ToArray();

foreach (var move in moves)
{
    if (part2Mode)
    {
        var temp = new List<char>();
        for (var i = 0; i < move.Quantity; i++)
        {
            temp.Add(stacks[move.From - 1].Pop());
        }

        foreach (var c in temp.Reverse<char>())
        {
            stacks[move.To - 1].Push(c);
        }
    }
    else
    {
        for (var i = 0; i < move.Quantity; i++)
        {
            var temp = stacks[move.From - 1].Pop();
            stacks[move.To - 1].Push(temp);
        }
    }
}

foreach (var stack in stacks)
{
    Console.Write(stack.ElementAt(0));
}
