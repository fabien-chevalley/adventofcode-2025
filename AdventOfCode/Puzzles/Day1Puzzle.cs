namespace AdventOfCode.Puzzles;

public class Day1Puzzle : Puzzle
{
    public override async ValueTask<long> PartOne()
    {
        long count = 0;
        long value = 50;
        var lines = await File.ReadAllLinesAsync(Filename);
        foreach (var line in lines)
        {
            if (line.StartsWith('L'))
            {
                value -= long.Parse(line[1..]) % 100;
            }

            if (line.StartsWith('R'))
            {
                
                value += long.Parse(line[1..]) % 100;
            }

            if (value < 0)
            {
                value += 100;
            }
            
            if (value > 99)
            {
                value -= 100;}

            if (value == 0)
            {
                count++;
            }
        }

        return count;
    }

    public override async ValueTask<long> PartTwo()
    {
        long count = 0;
        long value = 50;
        var lines = await File.ReadAllLinesAsync(Filename);
        foreach (var line in lines)
        {
            for (var i = 0; i < int.Parse(line[1..]); i++)
            {
                if (line.StartsWith('L'))
                    value--;

                if (line.StartsWith('R'))
                    value++;

                if (value % 100 == 0)
                    count++;
            }
        }

        return count;
    }
}
