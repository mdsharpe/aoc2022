﻿var fileRegex = new System.Text.RegularExpressions.Regex("(\\d+) ([\\w.]+)");

var input = await System.IO.File.ReadAllLinesAsync(args[0]);

var dirStack = new Stack<string>();
var allKnownDirs = new HashSet<string>();
var fs = new Dictionary<string, int>();

string GetCurrentPath() => "/" + string.Join('/', dirStack.Reverse<string>());

foreach (var line in input)
{
    switch (line)
    {
        case string cmd when cmd.StartsWith("$ "):
            switch (cmd.Substring(2))
            {
                case "cd ..":
                    dirStack.Pop();
                    break;

                case "cd /":
                    dirStack.Clear();
                    break;

                case string s when s.StartsWith("cd"):
                    dirStack.Push(s.Substring(3));
                    break;

                case "ls":
                    break;

                default:
                    throw new InvalidOperationException($"Unrecognized command '{cmd}'");
            }

            break;

        case string s when s.StartsWith("dir "):
            break;

        case string s when fileRegex.IsMatch(s):
            var match = fileRegex.Match(s);
            var size = int.Parse(match.Groups[1].Value);
            var filename = match.Groups[2].Value;
            var path = GetCurrentPath();
            if (!path.EndsWith('/')) path += '/';
            path += filename;
            fs.Add(path, size);
            break;

        default:
            throw new InvalidOperationException($"Unrecognized input '{line}'");
    }

    allKnownDirs.Add(GetCurrentPath());
}

var dirSizes = (from dir in allKnownDirs
                let files = fs.Where(o => o.Key.StartsWith(dir + "/"))
                select new
                {
                    Path = dir,
                    Size = files.Select(i => i.Value).DefaultIfEmpty().Sum()
                }).ToArray();

foreach (var item in dirSizes.OrderBy(o => o.Path))
{
    Console.WriteLine($"{item.Path}: {item.Size}");
}

Console.WriteLine(
    "Sum of sizes of directories with size at most 100000: "
    + dirSizes.Where(o => o.Size <= 100000).Select(o => o.Size).DefaultIfEmpty().Sum());
