// See https://aka.ms/new-console-template for more information
using MoreLinq;

var input = await System.IO.File.ReadAllLinesAsync(args[0]);

var rucksacks = input.Select(o => new Rucksack(o));

var commonItemPrioritiesSum = rucksacks
    .SelectMany(o => o.GetCommonItems())
    .Select(o => ItemPrioritisation.GetPriority(o))
    .Sum();

Console.WriteLine($"Sum of priorities of rucksack compartment commom items: {commonItemPrioritiesSum}");

var groupBadgesSum = rucksacks
    .Batch(3)
    .Select(o => new Group(o))
    .Select(o => o.GetBadge())
    .Select(o => ItemPrioritisation.GetPriority(o))
    .Sum();

Console.WriteLine($"Sum of priorities of group badges: {groupBadgesSum}");
