using MoreLinq;

static class StackParser
{
    public static List<Stack<char>> Parse(IEnumerable<string> input)
    {
        var stacks = new Dictionary<int, List<char>>();

        foreach (var line in input)
        {
            if (!line.Contains('['))
            {
                break;
            }

            foreach (var crate in line.Batch(4).Select((chars, stackIndex) => new
            {
                StackIndex = stackIndex,
                Str = new string(chars.ToArray()).Trim()
            }))
            {
                if (string.IsNullOrWhiteSpace(crate.Str))
                {
                    continue;
                }

                if (!crate.Str.StartsWith('[') || !crate.Str.EndsWith(']'))
                {
                    throw new InvalidOperationException();
                }

                if (!stacks.TryGetValue(crate.StackIndex, out var stack))
                {
                    stack = new List<char>();
                    stacks.Add(crate.StackIndex, stack);
                }

                stack.Add(crate.Str[1]);
            }
        }

        return stacks.OrderBy(o => o.Key).Select(o => new Stack<char>(o.Value.Reverse<char>())).ToList();
    }
}
