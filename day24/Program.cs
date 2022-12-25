var delay = (args.Length > 1 && int.TryParse(args[1], out var d)) ? d : 0;

var valley = new Valley(await System.IO.File.ReadAllLinesAsync(args[0]));

var minute = 0;

ConsoleMap.WriteMap(valley, minute);
await Task.Delay(delay);

do
{
    var delayTask = Task.Delay(delay);

    valley.Tick();
    minute++;

    ConsoleMap.WriteMap(valley, minute);

    await delayTask;
} while (!valley.GetOccupants(valley.Exit).OfType<Expedition>().Any());
