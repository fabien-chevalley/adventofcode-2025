#!/bin/bash

if [ -z "$1" ]; then
    echo "Usage: ./new-day.sh <day> [year]"
    echo "Example: ./new-day.sh 1"
    echo ""
    echo "To enable automatic input fetching:"
    echo "  export AOC_SESSION=\"your_session_cookie\""
    exit 1
fi

day=$1
year=${2:-2025}

day_padded=$(printf "%02d" $day)

if [ -f "AdventOfCode.csproj" ]; then
    puzzle_dir="Puzzles"
    input_dir="Inputs"
else
    puzzle_dir="AdventOfCode/Puzzles"
    input_dir="AdventOfCode/Inputs"
fi

puzzle_file="${puzzle_dir}/Day${day}Puzzle.cs"
input_file="${input_dir}/Day${day}Puzzle.input"

mkdir -p "$puzzle_dir"
mkdir -p "$input_dir"

if [ -f "$puzzle_file" ]; then
    echo "Day $day puzzle already exists!"
    exit 1
fi

cat > "$puzzle_file" << EOF
namespace AdventOfCode.Puzzles;

public class Day${day}Puzzle : Puzzle
{
    private string[] _lines = Array.Empty<string>();

    public override async ValueTask<long> PartOne()
    {
        _lines = await File.ReadAllLinesAsync(Filename);

        long result = 0;

        foreach (var line in _lines)
        {
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
}
EOF

if [ ! -f "$input_file" ] || [ ! -s "$input_file" ]; then
    if [ -n "$AOC_SESSION" ]; then
        echo "Fetching input from adventofcode.com..."

        url="https://adventofcode.com/${year}/day/${day}/input"

        if curl -s -f \
            -H "Cookie: session=${AOC_SESSION}" \
            -H "User-Agent: github.com/fabien-github (via shell script)" \
            -o "$input_file" \
            "$url"; then
            echo "Input fetched successfully"
        else
            echo "Failed to fetch input (puzzle may not be available yet)"
            echo "   Creating empty input file..."
            touch "$input_file"
        fi
    else
        echo "No AOC_SESSION set - creating empty input file"
        echo "   To enable auto-fetch: export AOC_SESSION=\"your_cookie\""
        touch "$input_file"
    fi
fi

echo ""
echo "Created Day $day puzzle:"
echo "   $puzzle_file"
echo "   $input_file"
echo ""

if [ -s "$input_file" ]; then
    input_size=$(wc -c < "$input_file" | tr -d ' ')
    echo "Ready to solve! Input file has $input_size bytes."
    echo "Run with: cd AdventOfCode && dotnet run"
else
    echo "Next steps:"
    echo "  1. Add your input to: $input_file"
    echo "  2. Implement your solution in: $puzzle_file"
    echo "  3. Run with: cd AdventOfCode && dotnet run"
fi