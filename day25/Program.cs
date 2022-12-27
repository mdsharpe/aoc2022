var input = await System.IO.File.ReadAllLinesAsync(args[0]);

var sum = 0L;

foreach (var snafu in input)
{
    Console.Write(snafu);
    var dec = SnafuConvert.ToInt64(snafu);
    Console.Write($" > {dec}");
    var backToSnafu = SnafuConvert.ToSnafuString(dec);
    Console.WriteLine($" > {backToSnafu}");

    if (backToSnafu != snafu)
    {
        throw new InvalidOperationException($"Snafu sanity check failed; '{backToSnafu}' != '{snafu}'");
    }

    sum += dec;
}

Console.WriteLine();

var sumSnafu = SnafuConvert.ToSnafuString(sum);

Console.WriteLine($"Sum = {sum} > {sumSnafu}");
