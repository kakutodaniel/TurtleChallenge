// See https://aka.ms/new-console-template for more information
//Console.WriteLine($"Hello, World!");

var settingsFile = args[0];
var movesFile = args[1];

string direction = "", exitPoint = "", initialDirection = "", startingPoint = "";
int boardSizeAxisX = default, boardSizeAxisY = default;
int startPointAxisX = default, startPointAxisY = default;
var minesCollection = new HashSet<string>();
bool reachExitTile;
string currentPoint;

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
    var boardSize = settingItems[0];
    startingPoint = settingItems[1];
    initialDirection = settingItems[2];
    exitPoint = settingItems[3];
    var mines = settingItems[4];

    minesCollection = mines.Split('|').ToHashSet();
    _ = int.TryParse(boardSize.Split('X')[0], out boardSizeAxisX);
    _ = int.TryParse(boardSize.Split('X')[1], out boardSizeAxisY);
}

void ReadMovesFile(string movesFile)
{
    var moveSequences = File.ReadAllLines(movesFile).Where(x => !string.IsNullOrWhiteSpace(x));

    for (int i = 0; i < moveSequences.Count(); i++)
    {
        direction = initialDirection;
        reachExitTile = false;

        _ = int.TryParse(startingPoint.Split(',')[0], out startPointAxisX);
        _ = int.TryParse(startingPoint.Split(',')[1], out startPointAxisY);

        var endState = false;
        var sequenceText = $"Sequence {i + 1}:";
        var moves = moveSequences.ElementAt(i).Split(',');

        foreach (var move in moves)
        {
            if (move.Equals("r", StringComparison.InvariantCultureIgnoreCase))
            {
                UpdateDirection();
            }
            else if (move.Equals("m", StringComparison.InvariantCultureIgnoreCase))
            {
                if (ExitSuccessfully())
                {
                    Console.WriteLine($"{sequenceText} Success!");
                    endState = !endState;
                    break;
                }

                UpdateMovement();

                CheckIfReachExitTile();

                if (HitMine())
                {
                    Console.WriteLine($"{sequenceText} Mine hit!");
                    endState = !endState;
                    break;
                }

                if (OffBoard())
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

void UpdateDirection()
{
    direction = direction switch
    {
        var d when d.Equals("north", StringComparison.InvariantCultureIgnoreCase) => "east",
        var d when d.Equals("east", StringComparison.InvariantCultureIgnoreCase) => "south",
        var d when d.Equals("south", StringComparison.InvariantCultureIgnoreCase) => "west",
        var d when d.Equals("west", StringComparison.InvariantCultureIgnoreCase) => "north",
    };
}

void UpdateMovement()
{
    _ = direction switch
    {
        var d when d.Equals("north", StringComparison.InvariantCultureIgnoreCase) => startPointAxisY--,
        var d when d.Equals("east", StringComparison.InvariantCultureIgnoreCase) => startPointAxisX++,
        var d when d.Equals("south", StringComparison.InvariantCultureIgnoreCase) => startPointAxisY++,
        var d when d.Equals("west", StringComparison.InvariantCultureIgnoreCase) => startPointAxisX--,
    };

    currentPoint = $"{startPointAxisX},{startPointAxisY}";
    //Console.WriteLine($"Position: {currentPoint}");
    //Console.WriteLine($"Direction: {direction}");
}

bool HitMine()
{
    return minesCollection.Contains(currentPoint);
}

bool OffBoard()
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

void CheckIfReachExitTile()
{
    reachExitTile = currentPoint == exitPoint;
}

bool ExitSuccessfully()
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
