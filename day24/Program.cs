const int speed = 250;
var input = await System.IO.File.ReadAllLinesAsync(args[0]);
var valley = new Valley(input);

var expedition = new Expedition { Coordinate = valley.GetEntrance() };
valley.Occupants.Add(expedition);

var moveTreeRoot = new MoveTreeNode();

ConsoleMap.WriteMap(valley);
await Task.Delay(speed);

do
{
    var delay = Task.Delay(speed);

    foreach (var actor in valley.Occupants)
    {
        actor.MoveWithin(valley);
    }

    ConsoleMap.WriteMap(valley);

    await delay;
} while (!expedition.Coordinate.Equals(valley.GetExit()));
