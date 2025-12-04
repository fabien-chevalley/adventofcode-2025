namespace AdventOfCode.Puzzles;

public class Day3Puzzle : Puzzle
{
    private string[] _lines = Array.Empty<string>();

    public override async ValueTask<long> PartOne()
    {
        _lines = await File.ReadAllLinesAsync(Filename);

        
        long result = 0;

        foreach (var line in _lines)
        {
            var digits = line.Select(x => int.Parse(x.ToString())).ToArray();
            // var sorted = digits
            //     .Select((value, index) => new { value, index })
            //     .OrderByDescending(x => x.value)
            //     .ToArray();
            //
            // var max = sorted.Take(2)
            //     .OrderBy(x => x.index)
            //     .Select(y => y.value)
            //     .ToArray();
            
            var max = digits
                .Select((value, index) => new { value, index })
                .OrderByDescending(x => x.value)
                .First();

            if (max.index == digits.Length - 1)
            {
                max = digits
                    .Take(digits.Length -1)
                    .Select((value, index) => new { value, index })
                    .OrderByDescending(x => x.value)
                    .First();
            }

            var second = digits
                .Skip(max.index + 1)
                .Max();

            result += max.value * 10L + second;
            // result += max.Aggregate(0L, (acc, digit) => acc * 10L + digit);

        }

        return result;
    }

    public override async ValueTask<long> PartTwo()
    {
        long result = 0;

        foreach (var line in _lines)
        {
        }

        return result;
    }
}
