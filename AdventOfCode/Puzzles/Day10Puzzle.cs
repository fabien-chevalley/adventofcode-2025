using Microsoft.Z3;

namespace AdventOfCode.Puzzles;

public class Day10Puzzle : Puzzle
{
    public override async ValueTask<long> PartOne()
    {
        long result = 0;

        var lines = await File.ReadAllLinesAsync(Filename);
        foreach (var line in lines)
        {
            var parts = line.Trim().Split(' ');

            var lights = parts[0][1..^1];
            var start = 0;
            for (var i = 0; i < lights.Length; i++)
            {
                if (lights[i] == '#')
                    start += 1 << i;
            }

            var buttons = parts[1..^1]
                .Select(wire => wire[1..^1].Split(',').Select(int.Parse))
                .Select(x => x.Sum(i => 1 << i))
                .ToList();

            var current = new HashSet<int> { start };
            var count = 0;

            while (!current.Contains(0))
            {
                var next = new HashSet<int>();
                foreach (var c in current)
                {
                    foreach (var button in buttons)
                    {
                        next.Add(c ^ button);
                    }
                }

                current = next;
                count++;
            }

            result += count;
        }

        return result;
    }

    public override async ValueTask<long> PartTwo()
    {
        long result = 0;
        using var ctx = new Context();

        var lines = await File.ReadAllLinesAsync(Filename);
        foreach (var line in lines)
        {
            var parts = line.Trim().Split(' ');
            var wiring = parts[1..^1];
            var joltage = parts[^1];

            var buttons = wiring
                .Select(wire => wire[1..^1].Split(',').Select(int.Parse).ToArray())
                .ToArray();

            var target = joltage[1..^1].Split(',').Select(int.Parse).ToArray();

            var buttonZ3 = Enumerable.Range(0, buttons.Length)
                .Select(i => ctx.MkIntConst($"button_{i}"))
                .ToArray<ArithExpr>();

            var optimize = ctx.MkOptimize();

            var constraints = target
                .Select((x, counter) => ctx.MkEq(
                    ctx.MkInt(x),
                    ctx.MkAdd(buttons
                        .Select((flips, btnIdx) => (flips, btnIdx))
                        .Where(x => x.flips.Contains(counter))
                        .Select(x => buttonZ3[x.btnIdx])
                        .DefaultIfEmpty(ctx.MkInt(0))
                        .ToArray())))
                .Concat(buttonZ3.Select(b => ctx.MkGe(b, ctx.MkInt(0))));

            optimize.Assert(constraints.ToArray());

            var totalZ3 = ctx.MkAdd(buttonZ3);
            optimize.MkMinimize(totalZ3);

            if (optimize.Check() == Status.SATISFIABLE)
            {
                if (optimize.Model.Evaluate(totalZ3) is IntNum intNum)
                    result += intNum.Int;
            }
        }

        return result;
    }
}