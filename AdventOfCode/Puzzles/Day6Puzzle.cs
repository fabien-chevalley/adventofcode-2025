namespace AdventOfCode.Puzzles;

public class Day6Puzzle : Puzzle
{
    private string[] _lines = Array.Empty<string>();

    public override async ValueTask<long> PartOne()
    {
        _lines = await File.ReadAllLinesAsync(Filename);

        var data = _lines
            .Take(_lines.Length - 1)
            .Select(line =>
                line.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(long.Parse)
                    .ToArray())
            .ToArray();

        var operators = _lines.Last().Split(" ", StringSplitOptions.RemoveEmptyEntries);

        long result = 0;

        var iteration = 0;
        foreach (var @operator in operators)
        {
            if (@operator == "+")
            {
                result += data.Select(row => row[iteration])
                    .Aggregate((x, y) => x + y);
            }

            if (@operator == "*")
            {
                result += data.Select(row => row[iteration])
                    .Aggregate((x, y) => x * y);
            }

            iteration++;
        }

        return result;
    }

    public override async ValueTask<long> PartTwo()
    {
        long result = 0;
        var values = new List<long>();
        var length = _lines[0].Length;
        for (var i = length - 1; i >= 0; i--)
        {
            values.Add(_lines
                .Take(_lines.Length - 1)
                .Select(row => row[i])
                .Where(x => x != ' ')
                .Select(x => int.Parse(x.ToString()))
                .Aggregate(0L, (acc, digit) => acc * 10L + digit));

            var op = _lines.Last()[i];
            if (op == ' ') continue;

            if (op == '+')
            {
                result += values.Aggregate((x, y) => x + y);
            }

            if (op == '*')
            {
                result += values.Aggregate((x, y) => x * y);
            }

            i--;
            values.Clear();
        }
        
        return result;
    }
}