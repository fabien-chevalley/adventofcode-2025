namespace AdventOfCode.Puzzles;

record Digit(int Value, int Index);

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
        }

        return result;
    }

    public override async ValueTask<long> PartTwo()
    {
        long result = 0;
        foreach (var line in _lines)
        {    
            var digits = line.Select(x => int.Parse(x.ToString())).ToArray();

            var selectedDigits = new List<Digit>();
            for (int i = 12; i > 0; i--)
            {
                var max = digits
                    .Select((value, index) => new Digit(value, index))
                    .Take(digits.Length - i + 1)
                    .Where(x => x.Index > (selectedDigits.Count == 0 ? 
                        -1 : 
                        selectedDigits.Select(y => y.Index).Max()))
                    .OrderByDescending(x => x.Value)
                    .First();
                
                selectedDigits.Add(max);
            }

            result += selectedDigits
                .Select(x => x.Value)
                .Aggregate(0L, (acc, digit) => acc * 10L + digit);
        }

        return result;
    }
}
