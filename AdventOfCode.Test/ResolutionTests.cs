using AdventOfCode.Puzzles;

namespace AdventOfCode.Test;

public class ResolutionTests
{
    [Test]
    [Arguments(typeof(Day1Puzzle), 982, 6106)]
    public async Task Year2024(Type type, long partOneResult, long partTwoResult)
    {
        if (Activator.CreateInstance(type) is not Puzzle puzzle) throw new InvalidOperationException();

        puzzle.Filename = $"../../../../AdventOfCode/Inputs/{type.Name}.input";
        await Assert.That(await puzzle.PartOne()).IsEqualTo(partOneResult);
        await Assert.That(await puzzle.PartTwo()).IsEqualTo(partTwoResult);
    }
}