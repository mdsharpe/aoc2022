// See https://aka.ms/new-console-template for more information

var input = await System.IO.File.ReadAllLinesAsync(args[0]);

var cur = 0;
var elves = new List<int>();

foreach (var line in input)
{
    if (int.TryParse(line, out var calories))
    {
        cur += calories;
    }
    else
    {
        elves.Add(cur);
        cur = 0;
    }
}

foreach (var elf in elves.OrderByDescending(o => o))
{
    Console.WriteLine(elf);
}

Console.WriteLine("top three: " + elves.OrderByDescending(o => o).Take(3).Sum());