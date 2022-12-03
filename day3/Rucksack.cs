class Rucksack
{
    public HashSet<char> Contents { get; private init; }
    public HashSet<char>[] Compartments { get; private init; }

    public Rucksack(string contents)
    {
        Contents = contents.ToHashSet();

        var compartmentLength = contents.Length / 2;
        Compartments = new[]
        {
            contents.Substring(0, compartmentLength).ToHashSet(),
            contents.Substring(compartmentLength, compartmentLength).ToHashSet()
        };
    }

    public IEnumerable<char> GetCommonItems()
    {
        return Compartments[0].Where(i => Compartments[1].Contains(i)).Distinct();
    }
}
