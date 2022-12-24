var input = await System.IO.File.ReadAllLinesAsync(args[0]);
var valley = new Valley(input);

ConsoleMap.WriteMap(valley);
await Task.Delay(1000);

do
{
    foreach (var actor in valley.Occupants)
    {
        actor.MoveWithin(valley);
    }

    ConsoleMap.WriteMap(valley);
    await Task.Delay(1000);
} while (true);
