internal class Expedition : Actor, IEquatable<Expedition>
{
    public override char ToChar() => 'E';


    public override bool Equals(object? obj)
    {
        if (!(obj is Expedition other))
        {
            return false;
        }

        return other.Coordinate == this.Coordinate;
    }
    public override void MoveWithin(Valley valley)
    {
        var nextGen = from d in Direction.EnumerateAll()
                      let c = Coordinate.Add(d)
                      where valley.GetCanOccupy(c)
                      select new Expedition
                      {
                          Coordinate = c
                      };

        valley.ExpeditionsNextGen.AddRange(nextGen);
    }

    public bool Equals(Expedition? other)
        => other != null && other.Coordinate == this.Coordinate;

    public override int GetHashCode() => HashCode.Combine(Coordinate);
}
