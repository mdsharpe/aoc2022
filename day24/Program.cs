const int speed = 0;
var input = await System.IO.File.ReadAllLinesAsync(args[0]);
var valley = new Valley(input);
valley.Expeditions.Add(new Expedition { Coordinate = valley.GetEntrance() });
var exit = valley.GetExit();

ConsoleMap.WriteMap(valley);
await Task.Delay(speed);

var minute = 0;
do
{
    var delay = Task.Delay(speed);

    foreach (var actor in valley.Occupants.ToArray())
    {
        actor.MoveWithin(valley);
    }

    valley.RevExpeditions();

    minute++;

    ConsoleMap.WriteMap(valley);

    await delay;
} while (!valley.Expeditions.Any(o => o.Coordinate == exit));

Console.WriteLine();
Console.WriteLine(minute.ToString());
