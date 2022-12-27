var input = await System.IO.File.ReadAllLinesAsync(args[0]);

var numbers = input.Select(o => new
{
    Snafu = o,
    Decimal = SnafuConvert.ToInt64(o),
    BackToSnafu = SnafuConvert.ToSnafuString(SnafuConvert.ToInt64(o))
}).ToArray();

foreach (var n in numbers)
{
    Console.WriteLine($"{n.Snafu} > {n.Decimal} > {n.BackToSnafu}");
}

var sum = numbers.Sum(o => o.Decimal);

var sumSnafu = SnafuConvert.ToSnafuString(sum);

Console.WriteLine(sumSnafu);
