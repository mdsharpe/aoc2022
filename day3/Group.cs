class Group
{
    public List<Rucksack> Rucksacks { get; private init; }

    public Group(IEnumerable<Rucksack> rucksacks)
    {
        Rucksacks = rucksacks.ToList();
    }

    public char GetBadge() => Rucksacks.First().Contents
        .Where(o => Rucksacks.Skip(1).All(r => r.Contents.Contains(o)))
        .Single();
}