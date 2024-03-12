// See https://aka.ms/new-console-template for more information

using System.Collections.Concurrent;

var settingsFile = args[0];
var movesFile = args[1];
var settingsDicionary = new ConcurrentDictionary<string, string>();

if (File.Exists(settingsFile))
{
    ReadSettingsFile(settingsFile);
}

if (File.Exists(movesFile))
{
    ReadMovesFile(movesFile);
}

void ReadSettingsFile(string settingsFile)
{
    var settings = File.ReadAllText(settingsFile);
    var settingItems = settings.Split(';');

    settingsDicionary.TryAdd("boardSize", settingItems[0]);
    settingsDicionary.TryAdd("startingPoint", settingItems[1]);
    settingsDicionary.TryAdd("initialDirection", settingItems[2]);
    settingsDicionary.TryAdd("exitPoint", settingItems[3]);
    settingsDicionary.TryAdd("mines", settingItems[4]);
}

void ReadMovesFile(string movesFile)
{
    var moveSequences = File.ReadAllLines(movesFile).Where(x => !string.IsNullOrWhiteSpace(x));

    Parallel.ForEach(
        moveSequences,
        new ParallelOptions { MaxDegreeOfParallelism = 3 },
        item =>
        {
            settingsDicionary.TryGetValue("boardSize", out var boardSize);
            settingsDicionary.TryGetValue("startingPoint", out var startingPoint);
            settingsDicionary.TryGetValue("initialDirection", out var direction);
            settingsDicionary.TryGetValue("exitPoint", out var exitPoint);
            settingsDicionary.TryGetValue("mines", out var mines);

            var minesCollection = mines?.Split('|').ToHashSet();
            bool reachExitTile = false, endState = false;

            _ = int.TryParse(boardSize?.Split('X')[0], out var boardSizeAxisX);
            _ = int.TryParse(boardSize?.Split('X')[1], out var boardSizeAxisY);

            _ = int.TryParse(startingPoint?.Split(',')[0], out var startPointAxisX);
            _ = int.TryParse(startingPoint?.Split(',')[1], out var startPointAxisY);

            var currentPoint = $"{startPointAxisX},{startPointAxisY}";
            var line = item.Split(';');
            var sequenceText = $"Sequence {line[0]}:";
            var moves = line[1].Split(',');

            foreach (var move in moves)
            {
                if (move.Equals("r", StringComparison.InvariantCultureIgnoreCase))
                {
                    UpdateDirection(ref direction);
                }
                else if (move.Equals("m", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (ExitSuccessfully(reachExitTile, direction))
                    {
                        Console.WriteLine($"{sequenceText} Success!");
                        endState = !endState;
                        break;
                    }

                    UpdateMovement(direction, ref startPointAxisX, ref startPointAxisY, ref currentPoint);

                    CheckIfReachExitTile(ref reachExitTile, currentPoint, exitPoint);

                    if (HitMine(minesCollection, currentPoint))
                    {
                        Console.WriteLine($"{sequenceText} Mine hit!");
                        endState = !endState;
                        break;
                    }

                    if (OffBoard(startPointAxisX, startPointAxisY, boardSizeAxisX, boardSizeAxisY))
                    {
                        Console.WriteLine($"{sequenceText} Moved off the board!");
                        endState = !endState;
                        break;
                    }
                }
            }

            if (!endState)
            {
                Console.WriteLine($"{sequenceText} Still in danger! Moves ran out!");
            }

            Console.WriteLine("----------------------------------------------");

        });
}

void UpdateDirection(ref string? direction)
{
    if (direction is null)
        return;

    direction = direction switch
    {
        var d when d.Equals("north", StringComparison.InvariantCultureIgnoreCase) => "east",
        var d when d.Equals("east", StringComparison.InvariantCultureIgnoreCase) => "south",
        var d when d.Equals("south", StringComparison.InvariantCultureIgnoreCase) => "west",
        var d when d.Equals("west", StringComparison.InvariantCultureIgnoreCase) => "north",
    };
}

void UpdateMovement(string? direction, ref int startPointAxisX, ref int startPointAxisY, ref string currentPoint)
{
    if (direction is null)
        return;

    _ = direction switch
    {
        var d when d.Equals("north", StringComparison.InvariantCultureIgnoreCase) => startPointAxisY--,
        var d when d.Equals("east", StringComparison.InvariantCultureIgnoreCase) => startPointAxisX++,
        var d when d.Equals("south", StringComparison.InvariantCultureIgnoreCase) => startPointAxisY++,
        var d when d.Equals("west", StringComparison.InvariantCultureIgnoreCase) => startPointAxisX--,
    };

    currentPoint = $"{startPointAxisX},{startPointAxisY}";
}

bool HitMine(HashSet<string>? minesCollection, string currentPoint)
{
    if (minesCollection is null)
        return false;
    
    return minesCollection.Contains(currentPoint);
}

bool OffBoard(int startPointAxisX, int startPointAxisY, int boardSizeAxisX, int boardSizeAxisY)
{
    if (startPointAxisX < 0 || startPointAxisY < 0)
        return true;

    if (startPointAxisX > boardSizeAxisX - 1)
        return true;

    if (startPointAxisY > boardSizeAxisY - 1)
        return true;

    return false;
}

void CheckIfReachExitTile(ref bool reachExitTile, string currentPoint, string? exitPoint)
{
    reachExitTile = currentPoint == exitPoint;
}

bool ExitSuccessfully(bool reachExitTile, string? direction)
{
    if (direction is null)
        return false;

    if (!reachExitTile)
        return false;

    if (reachExitTile && direction.Equals("east", StringComparison.InvariantCultureIgnoreCase))
        return true;

    return false;
}
