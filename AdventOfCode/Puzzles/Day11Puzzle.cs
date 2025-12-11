using Microsoft.Z3;

namespace AdventOfCode.Puzzles;

public class Day11Puzzle : Puzzle
{
    private Dictionary<string, string[]> _servers = new();

    public override async ValueTask<long> PartOne()
    {
        long result = 0;

        var lines = await File.ReadAllLinesAsync(Filename);
        _servers = lines.Select(line => line.Split(": "))
            .ToDictionary(
                parts => parts[0],
                parts => parts[1].Split(' '));

        var entry = "you";
        result = FindPath(entry);

        return result;
    }

    private long FindPath(string entry,  long count = 0)
    {
        foreach (var server in _servers[entry])
        {
            if (server == "out")
            {
                return count + 1;
            }

            count = FindPath(server, count);
        }

        return count;
    }

    public override async ValueTask<long> PartTwo()
    {
        var lines = await File.ReadAllLinesAsync(Filename);
        _servers = lines.Select(line => line.Split(": "))
            .ToDictionary(
                parts => parts[0],
                parts => parts[1].Split(' '));

        var entry = "svr";
        return FindPath2(entry, false, false);
    }

    private readonly Dictionary<(string, bool, bool), long> _memo = new();
    
    private long FindPath2(string node, bool hasFft, bool hasDac)
    {
        var state = (node, hasFft, hasDac);
        if (_memo.TryGetValue(state, out long cached))
        {
            return cached;
        }

        long count = 0;
        foreach (var nextNode in _servers[node])
        {
            bool newHasFft = hasFft || nextNode == "fft";
            bool newHasDac = hasDac || nextNode == "dac";

            if (nextNode == "out")
            {
                if (newHasFft && newHasDac)
                {
                    count++;
                }
            }
            else
            {
                count += FindPath2(nextNode, newHasFft, newHasDac);
            }
        }

        _memo[state] = count;
        return count;
    }
}