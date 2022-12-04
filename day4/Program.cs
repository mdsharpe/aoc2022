var input = await System.IO.File.ReadAllLinesAsync(args[0]);

int[] ParseRange(string str)
{
    var start = int.Parse(str.Split('-')[0]);
    var end = int.Parse(str.Split('-')[1]);
    return Enumerable.Range(start, end - (start - 1)).ToArray();
}

bool GetFullyContains(IEnumerable<int> a, IEnumerable<int> b)
    => a.All(b.Contains) || b.All(a.Contains);

bool GetOverlapsAtAll(IEnumerable<int> a, IEnumerable<int> b)
    => a.Any(b.Contains);

int countFullyContains = 0;
int countOverlapsAtAll = 0;

foreach (var pairString in input)
{
    var ranges = pairString.Split(',').Select(ParseRange).ToArray();

    if (GetFullyContains(ranges[0], ranges[1]))
    {
        countFullyContains++;
    }

    if (GetOverlapsAtAll(ranges[0], ranges[1]))
    {
        countOverlapsAtAll++;
    }
}

Console.WriteLine(countFullyContains.ToString());
Console.WriteLine(countOverlapsAtAll.ToString());
