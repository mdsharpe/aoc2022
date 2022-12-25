var delay = (args.Length > 1 && int.TryParse(args[1], out var d)) ? d : 0;
var journeys = (args.Length > 2 && int.TryParse(args[2], out var j)) ? j : 1;

var valley = new Valley(await System.IO.File.ReadAllLinesAsync(args[0]));

var minute = 0;

ConsoleMap.WriteMap(valley, minute);
await Task.Delay(delay);

var travellingToExit = true;

for (var journey = 0; journey <= journeys - 1; journey++)
{
    var goal = travellingToExit ? valley.Exit : valley.Entrance;

    do
    {
        var delayTask = Task.Delay(delay);

        valley.Tick();
        minute++;

        ConsoleMap.WriteMap(valley, minute);

        await delayTask;
    } while (!valley.GetOccupants(goal).OfType<Expedition>().Any());

    if (journey < journeys - 1) 
    {
        valley.Expeditions.RemoveAll(o => o.Coordinate != goal);
        travellingToExit = !travellingToExit;
    }
}
