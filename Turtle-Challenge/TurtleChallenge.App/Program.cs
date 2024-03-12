// See https://aka.ms/new-console-template for more information
//Console.WriteLine($"Hello, World!");

var settingsFile = args[0];
var movesFile = args[1];

string boardSizeKey = "boardSize", startingPointKey = "startingPoint",
        initialDirectionKey = "initialDirection", exitPointKey = "exitPoint",
        minesKey = "mines";

var settingsDictionary = new Dictionary<string, string>();

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

    settingsDictionary.Add(boardSizeKey, settingItems[0]);
    settingsDictionary.Add(startingPointKey, settingItems[1]);
    settingsDictionary.Add(initialDirectionKey, settingItems[2]);
    settingsDictionary.Add(exitPointKey, settingItems[3]);
    settingsDictionary.Add(minesKey, settingItems[4]);
}

void ReadMovesFile(string movesFile)
{
    var moveSequences = File.ReadAllLines(movesFile).Where(x => !string.IsNullOrWhiteSpace(x));
    var boardSize = settingsDictionary[boardSizeKey];
    var minesCollection = settingsDictionary[minesKey].Split('|').ToHashSet();
    var exitPoint = settingsDictionary[exitPointKey];

    _ = int.TryParse(boardSize.Split('x')[0], out var boardSizeAxisX);
    _ = int.TryParse(boardSize.Split('x')[1], out var boardSizeAxisY);

    for (int i = 0; i < moveSequences.Count(); i++)
    {
        var reachedExitTile = false;
        var endState = false;
        var direction = settingsDictionary[initialDirectionKey];
        var startingPoint = settingsDictionary[startingPointKey];

        _ = int.TryParse(startingPoint.Split(',')[0], out var startPointAxisX);
        _ = int.TryParse(startingPoint.Split(',')[1], out var startPointAxisY);
        var currentPoint = $"{startPointAxisX},{startPointAxisY}";

        var sequenceText = $"Sequence {i + 1}:";
        var moves = moveSequences.ElementAt(i).Split(',');

        foreach (var move in moves)
        {
            if (move.Equals("r", StringComparison.InvariantCultureIgnoreCase))
            {
                UpdateDirection(ref direction);
            }
            else if (move.Equals("m", StringComparison.InvariantCultureIgnoreCase))
            {
                if (ExitSuccessfully(reachedExitTile, direction))
                {
                    Console.WriteLine($"{sequenceText} Success!");
                    endState = !endState;
                    break;
                }

                UpdateMovement(direction, ref startPointAxisX, ref startPointAxisY, ref currentPoint);

                reachedExitTile = ReachedExitTile(currentPoint, exitPoint);

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
    }
}

void UpdateDirection(ref string direction)
{
    direction = direction switch
    {
        var d when d.Equals("north", StringComparison.InvariantCultureIgnoreCase) => "east",
        var d when d.Equals("east", StringComparison.InvariantCultureIgnoreCase) => "south",
        var d when d.Equals("south", StringComparison.InvariantCultureIgnoreCase) => "west",
        var d when d.Equals("west", StringComparison.InvariantCultureIgnoreCase) => "north",
    };
}

void UpdateMovement(string direction, ref int startPointAxisX, ref int startPointAxisY, ref string currentPoint)
{
    _ = direction switch
    {
        var d when d.Equals("north", StringComparison.InvariantCultureIgnoreCase) => startPointAxisY--,
        var d when d.Equals("east", StringComparison.InvariantCultureIgnoreCase) => startPointAxisX++,
        var d when d.Equals("south", StringComparison.InvariantCultureIgnoreCase) => startPointAxisY++,
        var d when d.Equals("west", StringComparison.InvariantCultureIgnoreCase) => startPointAxisX--,
    };

    currentPoint = $"{startPointAxisX},{startPointAxisY}";
}

bool HitMine(HashSet<string> minesCollection, string currentPoint)
{
    return minesCollection.Contains(currentPoint);
}

bool OffBoard(int startPointAxisX, int startPointAxisY, int boardSizeAxisX, int boardSizeAxisY)
{
    if (startPointAxisX < 0 || startPointAxisY < 0)
    {
        return true;
    }

    if (startPointAxisX > boardSizeAxisX - 1)
    {
        return true;
    }

    if (startPointAxisY > boardSizeAxisY - 1)
    {
        return true;
    }

    return false;
}

bool ReachedExitTile(string currentPoint, string exitPoint)
{
    return currentPoint == exitPoint;
}

bool ExitSuccessfully(bool reachExitTile, string direction)
{
    if (!reachExitTile)
    {
        return false;
    }

    if (reachExitTile && direction.Equals("east", StringComparison.InvariantCultureIgnoreCase))
    {
        return true;
    }

    return false;
}
