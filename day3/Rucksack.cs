class Rucksack
{
    public string Contents { get; init; }

    public string[] GetCompartmentContents()
    {
        var halfLength = Contents.Length / 2;
        return new[] {
            Contents.Substring(0, halfLength),
            Contents.Substring(halfLength, halfLength)
        };
    }

    public IEnumerable<char> GetCommonItems()
    {
        var compartments = GetCompartmentContents();
        return compartments[0].Where(i => compartments[1].Contains(i)).Distinct();
    }
}
