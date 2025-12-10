using MathNet.Spatial.Euclidean;

namespace AdventOfCode.Puzzles;

public class Day8Puzzle : Puzzle
{
    private string[] _lines = Array.Empty<string>();

    public override async ValueTask<long> PartOne()
    {
        _lines = await File.ReadAllLinesAsync(Filename);

        var points = _lines
            .Select(l => l.Split(",")
                .Select(int.Parse)
                .ToArray())
            .Select(p => new Point3D(p[0], p[1], p[2]))
            .ToArray();

        var distances = points
            .SelectMany((p1, i) => points.Skip(i + 1).Select((p2, j) => new
            {
                Index1 = i,
                Index2 = i + j + 1,
                Point1 = p1,
                Point2 = p2,
                Distance = p1.DistanceTo(p2)
            }))
            .OrderBy(x => x.Distance)
            .ToArray();


        List<HashSet<int>> network = [];
        foreach (var distance in distances.Take(1000))
        {
            var networks = network.Where(x => x.Overlaps([distance.Index1, distance.Index2])).ToList();
            switch (networks.Count)
            {
                case 2:
                    network.RemoveAll(c => networks.Contains(c));
                    network.Add(networks[0].Union(networks[1]).ToHashSet());
                    break;
                case 1:
                    networks.First().Add(distance.Index1);
                    networks.First().Add(distance.Index2);
                    break;
                default:
                    network.Add([distance.Index1, distance.Index2]);
                    break;
            }
        }

        return network
            .OrderByDescending(x => x.Count)
            .Take(3)
            .Aggregate(1L, (acc, val) => acc * val.Count);
    }

    public override async ValueTask<long> PartTwo()
    {
        _lines = await File.ReadAllLinesAsync(Filename);

        var points = _lines
            .Select(l => l.Split(",")
                .Select(int.Parse)
                .ToArray())
            .Select(p => new Point3D(p[0], p[1], p[2]))
            .ToArray();

        var distances = points
            .SelectMany((p1, i) => points.Skip(i + 1).Select((p2, j) => new
            {
                Index1 = i,
                Index2 = i + j + 1,
                Point1 = p1,
                Point2 = p2,
                Distance = p1.DistanceTo(p2)
            }))
            .OrderBy(x => x.Distance)
            .ToArray();


        List<HashSet<int>> network = [];
        foreach (var distance in distances)
        {
            var networks = network.Where(x => x.Overlaps([distance.Index1, distance.Index2])).ToList();
            switch (networks.Count)
            {
                case 2:
                    network.RemoveAll(c => networks.Contains(c));
                    network.Add(networks[0].Union(networks[1]).ToHashSet());
                    break;
                case 1:
                    networks.First().Add(distance.Index1);
                    networks.First().Add(distance.Index2);
                    break;
                default:
                    network.Add([distance.Index1, distance.Index2]);
                    break;
            }

            if(network.First().Count == _lines.Length)
                return long.Parse(_lines[distance.Index1].Split(",")[0]) * long.Parse(_lines[distance.Index2].Split(",")[0]);
        }

        return -1;
    }
}