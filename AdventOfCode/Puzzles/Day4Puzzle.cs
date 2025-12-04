using AdventOfCode.Helpers;
using AdventOfCode.Models;

namespace AdventOfCode.Puzzles;

public class Day4Puzzle : Puzzle
{
    private string[] _lines = Array.Empty<string>();

    public override async ValueTask<long> PartOne()
    {
        _lines = await File.ReadAllLinesAsync(Filename);

        var matrix = new Matrix(_lines
            .Select(l => l.Select(c => c).ToArray())
            .ToArray()
            .ConvertJaggedToRectangular());

        long result = 0;

        foreach (var position in matrix.Where(c => matrix[c] == '@'))
        {
            var neighbors = position.Neighbors(1);
            var count = neighbors.Count(c => !matrix.IsOutOfBox(c) && matrix[c] == '@');
            if (count <= 3)
            {
                result++;
            }
        }

        return result;
    }

    public override async ValueTask<long> PartTwo()
    {
        _lines = await File.ReadAllLinesAsync(Filename);

        var matrix = new Matrix(_lines
            .Select(l => l.Select(c => c).ToArray())
            .ToArray()
            .ConvertJaggedToRectangular());

        long result = 0;

        var previousCount = matrix.Count(c => matrix[c] == '@');

        while (true)
        {
            foreach (var position in matrix.Where(c => matrix[c] == '@'))
            {
                var neighbors = position.Neighbors(1);
                var count = neighbors.Count(c => !matrix.IsOutOfBox(c) && matrix[c] == '@');
                if (count <= 3)
                {
                    matrix[position] = '.';
                    result++;
                }
            } 
            
            if (previousCount == matrix.Count(c => matrix[c] == '@'))
            {
                break;
            }

            previousCount = matrix.Count(c => matrix[c] == '@');
        }
       

        return result;
    }
}