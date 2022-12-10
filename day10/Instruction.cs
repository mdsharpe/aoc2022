using System.Collections.Immutable;
using System.Text.RegularExpressions;

class Instruction
{
    private readonly Regex AddxRegex = new Regex("addx (-?\\d+)");

    public ImmutableList<Action<Cpu>> Actions { get; init; }

    public Instruction(string instruction)
    {
        switch (instruction)
        {
            case string addx when AddxRegex.IsMatch(addx):
                var match = AddxRegex.Match(addx);
                var x = int.Parse(match.Groups[1].Value);
                Actions = ImmutableList.Create<Action<Cpu>>(
                    cpu => { },
                    cpu => cpu.X += x
                );
                break;

            case "noop":
                Actions = ImmutableList.Create<Action<Cpu>>(
                    cpu => { }
                );
                break;

            default:
                throw new InvalidOperationException();
        }
    }
}
