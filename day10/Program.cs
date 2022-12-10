var program = (await System.IO.File.ReadAllLinesAsync(args[0]))
    .Select(i => new Instruction(i));

var cpu = new Cpu();
var cycle = 0;
var interestingSignalStrengths = new List<int>();

foreach (var action in program.SelectMany(o => o.Actions))
{
    cycle++;

    if (cycle == 20 || (cycle - 20) % 40 == 0)
    {
        interestingSignalStrengths.Add(cycle * cpu.X);
    }

    var crtX = ((cycle - 1) % 40) + 1;
    var spritePositioned = cpu.X >= crtX - 2 && cpu.X <= crtX;
    Console.Write(spritePositioned ? '#' : '.');
    if (crtX == 40)
    {
        Console.WriteLine();
    }

    action(cpu);
}

Console.WriteLine();
Console.WriteLine($"Sum of interesting signal strengths: {interestingSignalStrengths.Sum()}");
