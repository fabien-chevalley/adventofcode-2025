# Advent of Code 2025

C# solution for Advent of Code 2025.

## Setup

```bash
dotnet restore
```

### Optional: Enable Automatic Input Fetching

To automatically download puzzle inputs from adventofcode.com:

1. Log in to https://adventofcode.com
2. Open browser DevTools (F12)
3. Go to Application/Storage → Cookies
4. Copy the value of the `session` cookie
5. Set the environment variable:

```bash
export AOC_SESSION="your_session_cookie_value_here"
```

To make it permanent, add it to your `~/.zshrc` or `~/.bashrc`:
```bash
echo 'export AOC_SESSION="your_cookie"' >> ~/.zshrc
```

## Creating a New Puzzle

Use the `new-day.sh` script to create a new day's puzzle:

```bash
./new-day.sh <day> [year]
```

Examples:
```bash
./new-day.sh 1        # Creates Day 1 puzzle for 2025
./new-day.sh 5 2024   # Creates Day 5 puzzle for 2024
```

This will:
- Create `AdventOfCode/Puzzles/Day{N}Puzzle.cs` - Your puzzle solution file
- Create `AdventOfCode/Inputs/Day{N}Puzzle.input` - Input file (auto-downloaded if AOC_SESSION is set)

## Implementing Solutions

1. Add your puzzle input to `AdventOfCode/Inputs/Day{N}Puzzle.input`
2. Implement `PartOne()` and `PartTwo()` in the generated puzzle class
3. Run to see results

## Running Solutions

```bash
cd AdventOfCode
dotnet run
```

Or from the project root:
```bash
dotnet run --project AdventOfCode
```

## Project Structure

```
AdventOfCode/
├── Puzzles/         # Your puzzle implementations (committed to git)
├── Inputs/          # Puzzle inputs (not committed)
├── Models/          # Shared models (Coordinates, Direction, Matrix)
├── Helpers/         # Helper extensions
├── Program.cs       # Entry point
├── Puzzle.cs        # Base puzzle class
└── Solver.cs        # Puzzle execution logic
```

## Features

- **Clean puzzle generation**: Physical files you can edit and commit
- **Automatic input fetching**: Download puzzle inputs directly from adventofcode.com (optional)
- **Helper methods**: Template includes common AoC patterns
- **Automatic input copying**: Inputs copied to output directory during build
- **Pretty console output**: Uses Spectre.Console for nice tables

## Notes

- Puzzle input files (`*.input`) are excluded from git to respect Advent of Code's [copyright](https://adventofcode.com/about)
- Your session cookie is personal - never commit it to git
- The script respects adventofcode.com's servers with proper User-Agent headers