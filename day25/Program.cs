var input = await System.IO.File.ReadAllLinesAsync(args[0]);

var sum = 0L;

foreach (var snafu in input)
{
    var dec = SnafuConvert.ToInt64(snafu);
    var backToSnafu = SnafuConvert.ToSnafuString(dec);
    Console.WriteLine($"{snafu} > {dec} > {backToSnafu}");
}

Console.WriteLine();

var sumSnafu = SnafuConvert.ToSnafuString(sum);

Console.WriteLine($"Sum = {sum} > {sumSnafu}");
