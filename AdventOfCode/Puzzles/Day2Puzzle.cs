namespace AdventOfCode.Puzzles;

public class Day2Puzzle : Puzzle
{
    private string[] _lines = Array.Empty<string>();

    public override async ValueTask<long> PartOne()
    {
        _lines = await File.ReadAllLinesAsync(Filename);

        long result = 0;

        foreach (string range in _lines.SelectMany(x => x.Split(",")))
        {
            var parts = range.Split('-');
            var start = long.Parse(parts[0]);
            var end = long.Parse(parts[1]);

            for (long i = start; i <= end; i++)
            {
                var digits = GetDigits(i).ToArray();
                if (digits.Length % 2 == 0)
                {
                    long result1 = digits
                        .Take(digits.Length / 2).ToArray()
                        .Aggregate(0L, (acc, digit) => acc * 10L + digit);  // 12345
                    long result2 = digits
                        .TakeLast(digits.Length / 2).ToArray()
                        .Aggregate(0L, (acc, digit) => acc * 10L + digit);  // 12345

                    if (result1 == result2)
                    {
                        result += i;
                    }
                }
            }
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
    
    public static IEnumerable<long> GetDigits(long source)
    {
        long individualFactor = 0;
        long tennerFactor = Convert.ToInt64(Math.Pow(10, source.ToString().Length));
        do
        {
            source -= tennerFactor * individualFactor;
            tennerFactor /= 10;
            individualFactor = source / tennerFactor;

            yield return individualFactor;
        } while (tennerFactor > 1);
    }
}
