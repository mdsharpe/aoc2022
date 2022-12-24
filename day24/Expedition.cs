internal class Expedition : Actor
{
    public override char ToChar() => 'E';

    public ISet<Coordinate> NextStepsTried { get; set; } = new HashSet<Coordinate>();

    public override void MoveWithin(Valley valley)
    {
        var possibleMoves = from d in Direction.EnumerateAll()
                            let c = Coordinate.Add(d)
                            where d.Equals(Direction.Wait)
                                || valley.GetCanOccupy(c)
                                || c.Equals(valley.GetExit())
                            where !NextStepsTried.Contains(c)
                            select c;

        Coordinate = possibleMoves.First();
    }
}
