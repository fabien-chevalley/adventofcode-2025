using MathNet.Spatial.Euclidean;

namespace AdventOfCode.Puzzles;

public class Day9Puzzle : Puzzle
{
    private string[] _lines = Array.Empty<string>();

    public override async ValueTask<long> PartOne()
    {
        _lines = await File.ReadAllLinesAsync(Filename);

        var points = _lines
            .Select(l => l.Split(",")
                .Select(int.Parse)
                .ToArray())
            .Select(p => new Point2D(p[0], p[1]))
            .ToArray();

        var surfaces = points
            .SelectMany((p1, i) => points.Skip(i + 1).Select((p2, j) => new
            {
                Index1 = i,
                Index2 = i + j + 1,
                Point1 = p1,
                Point2 = p2,
                Surface = (Math.Abs(p1.X - p2.X) + 1) * (Math.Abs(p1.Y - p2.Y) + 1)
            }))
            .OrderByDescending(x => x.Surface)
            .ToArray();


        return (long)surfaces.First().Surface;
    }

    public override async ValueTask<long> PartTwo()
    {
        _lines = await File.ReadAllLinesAsync(Filename);

        var corners = _lines
            .Select(l => l.Split(",")
                .Select(int.Parse)
                .ToArray())
            .Select(p => new Point2D(p[0], p[1]))
            .ToArray();


        var polygon = new RectilinearPolygon(corners);

        var result = corners
            .SelectMany((c1, i) => corners.Skip(i + 1).Select(c2 => new Rect(c1, c2)))
            .Where(polygon.Contains)
            .Select(rect => rect.Area)
            .DefaultIfEmpty(0L)
            .Max();

        return result;
    }
}

public sealed class RectilinearPolygon
{
    private readonly Point2D[] _vertices;
    private readonly int[] _criticalX;
    private readonly int[] _criticalY;
    private readonly Dictionary<(int X, int Y), bool> _cache = new();

    public RectilinearPolygon(Point2D[] vertices)
    {
        _vertices = vertices;
        _criticalX = vertices.Select(v => (int)v.X).Distinct().Order().ToArray();
        _criticalY = vertices.Select(v => (int)v.Y).Distinct().Order().ToArray();
    }

    public bool Contains(Rect rect)
    {
        if (!ContainsPoint(rect.MinX, rect.MinY) ||
            !ContainsPoint(rect.MaxX, rect.MaxY) ||
            !ContainsPoint(rect.MaxX, rect.MinY) ||
            !ContainsPoint(rect.MinX, rect.MaxY))
        {
            return false;
        }

        if (_criticalY.Where(y => y >= rect.MinY && y <= rect.MaxY).Any(y => !ContainsPoint(rect.MinX, y) || !ContainsPoint(rect.MaxX, y)))
        {
            return false;
        }

        return _criticalX.Where(x => x >= rect.MinX && x <= rect.MaxX).All(x => ContainsPoint(x, rect.MinY) && ContainsPoint(x, rect.MaxY));
    }

    private bool ContainsPoint(int x, int y)
    {
        if (_cache.TryGetValue((x, y), out var result))
        {
            return result;
        }

        return _cache[(x, y)] = IsPointInsideOrOnBoundary(x, y);
    }

    private bool IsPointInsideOrOnBoundary(int x, int y)
    {
        var intersections = 0;

        for (var i = 0; i < _vertices.Length; i++)
        {
            var v1 = _vertices[i];
            var v2 = _vertices[(i + 1) % _vertices.Length];

            if (v1.X == v2.X)
            {
                if (x == v1.X && IsBetween(y, (int)v1.Y, (int)v2.Y, maxInclusive: true))
                {
                    return true;
                }

                if (IsBetween(y, (int)v1.Y, (int)v2.Y, maxInclusive: false) && x < v1.X)
                {
                    intersections++;
                }
            }
            else if (y == v1.Y && IsBetween(x, (int)v1.X, (int)v2.X, maxInclusive: true))
            {
                return true;
            }
        }

        return (intersections & 1) == 1;
    }

    private static bool IsBetween(int value, int a, int b, bool maxInclusive)
    {
        var (min, max) = a < b ? (a, b) : (b, a);
        return maxInclusive
            ? value >= min && value <= max
            : value >= min && value < max;
    }
}

public sealed class Rect
{
    public int MinX { get; }
    public int MaxX { get; }
    public int MinY { get; }
    public int MaxY { get; }
    public long Area => (long)(MaxX - MinX + 1) * (MaxY - MinY + 1);

    public Rect(Point2D corner1, Point2D corner2)
    {
        MinX = (int)Math.Min(corner1.X, corner2.X);
        MaxX = (int)Math.Max(corner1.X, corner2.X);
        MinY = (int)Math.Min(corner1.Y, corner2.Y);
        MaxY = (int)Math.Max(corner1.Y, corner2.Y);
    }
}