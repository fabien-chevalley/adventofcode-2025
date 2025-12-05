using System.Text.RegularExpressions;
using AdventOfCode.Helpers;
using AdventOfCode.Models;

namespace AdventOfCode.Puzzles;

public class Day5Puzzle : Puzzle
{
    private string[] _lines = Array.Empty<string>();

    public override async ValueTask<long> PartOne()
    {
        var matches = Regex.Matches(File.ReadAllText(Filename), @"(?<range>\d+\-\d+)|(?<input>\d+)");

        var ranges = matches
            .OfType<Match>()
            .Where(m => m.Groups["range"].Success)
            .Select(m => m.Groups["range"].Value
                .Split('-')
                .Select(long.Parse)
                .ToArray())
            .ToArray();


        long result = 0;

        foreach (var item in matches.Where(m => m.Groups["input"].Success))
        {
            var value = long.Parse(item.Value);
            if (ranges.Any(x => value >= x[0] && value <= x[1]))
            {
                result++;
            }
        }

        return result;
    }

    public override async ValueTask<long> PartTwo()
    {
        var matches = Regex.Matches(File.ReadAllText(Filename), @"(?<range>\d+\-\d+)|(?<input>\d+)");

        var ranges = matches
            .OfType<Match>()
            .Where(m => m.Groups["range"].Success)
            .Select(m => m.Groups["range"].Value
                .Split('-')
                .Select(long.Parse)
                .ToArray())
            .OrderBy(x => x[0]).ThenBy(x => x[1])
            .ToArray();


        long result = 0;

        List<long[]> done = [];
        foreach (var range in ranges)
        {
            if (done.Any(x => x[1] >= range[1]))
            {
                continue;
            }

            var overlap = done.Where(x => x[1] >= range[0]).ToArray();
            if (overlap.Length != 0)
            {
                result += range[1] - overlap.Last()[1];
            }
            else
            {
                result += range[1] - range[0] + 1;
            }
            
            done.Add(range);
        }

        return result;
    }
}