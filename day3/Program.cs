// See https://aka.ms/new-console-template for more information

var input = await System.IO.File.ReadAllLinesAsync(args[0]);

var rucksacks = input.Select(o => new Rucksack { Contents = o });

var commonItems = rucksacks
    .SelectMany(o => o.GetCommonItems())
    .Select(o => new
    {
        Item = o,
        Priority = ItemPrioritisation.GetPriority(o)
    })
    .ToArray();

foreach (var item in commonItems)
{
    Console.WriteLine($"{item.Priority} ({item.Item})");
}

var sum = commonItems.Select(o => o.Priority).Sum();

Console.WriteLine(sum);
