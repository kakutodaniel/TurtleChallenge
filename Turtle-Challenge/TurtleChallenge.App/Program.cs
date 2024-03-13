// See https://aka.ms/new-console-template for more information
//Console.WriteLine($"Hello, World!");

using TurtleChallenge.App.Domain;
using TurtleChallenge.App.Enums;
using TurtleChallenge.App.Parsers;

var settingsFile = args[0];
var movesFile = args[1];
Settings? settings = default;
Moves? moves = default;

if (File.Exists(settingsFile))
{
    var settingsLine = File.ReadAllText(settingsFile);

    settings = SettingsParser.Parse(settingsLine);
}

if (File.Exists(movesFile))
{
    var moveSequences = File.ReadAllLines(movesFile).Where(x => !string.IsNullOrWhiteSpace(x));

    moves = MovesParser.Parse(moveSequences);
}

var turtleGame = new TurtleGame(settings);

for (int i = 0; i < moves?.Movements.Count; i++)
{
    var sequenceText = $"Sequence {i + 1}:";
    var initialDirection = settings.InitialDirection;
    var startPointPosition = new Position(settings.StartPointPosition);

    var turtle = new Turtle(initialDirection, startPointPosition);
    var result = turtleGame.Run(turtle, moves.Movements[i]);

    switch (result)
    {
        case Result.Success:
            Console.WriteLine($"{sequenceText} Success!");
            break;
        case Result.MineHit:
            Console.WriteLine($"{sequenceText} Mine hit!");
            break;
        case Result.MovedOffBoard:
            Console.WriteLine($"{sequenceText} Moved off the board!");
            break;
        case Result.MovesRanOut:
            Console.WriteLine($"{sequenceText} Still in danger!");
            break;
        default:
            Console.WriteLine($"{sequenceText} Not implemented result");
            break;
    }
}
