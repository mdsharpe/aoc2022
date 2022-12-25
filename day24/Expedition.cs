internal class Expedition : Actor
{
    public override char ToChar() => 'E';

    public override void MoveWithin(Valley valley)
    {
        var nxtGen = (from d in Direction.EnumerateAll()
                      let c = Coordinate.Add(d)
                      where valley.GetCanOccupy(c)
                          || c == valley.GetExit()
                      select new Expedition
                      {
                          Coordinate = c
                      }).ToArray();

        if (!nxtGen.Any())
        {
            nxtGen = new[] { this };
        }

        valley.ExpeditionsNxtGen.AddRange(nxtGen);
    }
}
