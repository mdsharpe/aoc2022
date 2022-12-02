// See https://aka.ms/new-console-template for more information

var input = await System.IO.File.ReadAllLinesAsync(args[0]);

var secondColIsOutcome = args.Length > 1;

var gamesEnumerable = input.Select(o => RockPaperScissorsGame.Parse(o, secondColIsOutcome));

var totalScore = gamesEnumerable.Select(o => o.Score).DefaultIfEmpty().Sum();

Console.WriteLine($"Total score: {totalScore}");