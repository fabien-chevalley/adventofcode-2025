namespace AdventOfCode;

public interface IPuzzle;

public abstract class Puzzle : IPuzzle
{
    public string Filename { get; set; } = string.Empty;

    public abstract ValueTask<long> PartOne();

    public abstract ValueTask<long> PartTwo();
}