var input = await System.IO.File.ReadAllTextAsync(args[0]);
var markerLength = int.Parse(args[1]);

for (var i = markerLength - 1; i < input.Length; i++)
{
    if (input.Substring(i - (markerLength - 1), markerLength).Distinct().Count() == markerLength)
    {
        Console.WriteLine($"Marker found after {i + 1} characters arrived.");
        break;
    }
}
