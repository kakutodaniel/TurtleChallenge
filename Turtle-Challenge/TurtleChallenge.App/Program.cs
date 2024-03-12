// See https://aka.ms/new-console-template for more information
//Console.WriteLine($"Hello, World!");

using TurtleChallenge.App.Domain;
using TurtleChallenge.App.Enums;
using TurtleChallenge.App.Parsers;

var settingsFile = args[0];
var movesFile = args[1];
Settings? settingsDomain = default;
Moves? movesDomain = default;

if (File.Exists(settingsFile))
{
    var settings = File.ReadAllText(settingsFile);

    settingsDomain = SettingsParser.Parse(settings);
}

if (File.Exists(movesFile))
{
    var moveSequences = File.ReadAllLines(movesFile).Where(x => !string.IsNullOrWhiteSpace(x));

    movesDomain = MovesParser.Parse(moveSequences);
}

var turtleGame = new TurtleGame(settingsDomain);

for (int i = 0; i < movesDomain.Movements.Count; i++)
{
    var sequenceText = $"Sequence {i + 1}:";
    var initialDirection = settingsDomain.InitialDirection;
    var startPointPosition = new Position(settingsDomain.StartPointPosition.AxisX, settingsDomain.StartPointPosition.AxisY);

    var turtle = new Turtle(initialDirection, startPointPosition);
    var result = turtleGame.Run(turtle, movesDomain.Movements[i]);

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
            Console.WriteLine($"{sequenceText} Still in danger! Moves ran out!");
            break;
        default:
            Console.WriteLine($"{sequenceText} Not implemented result");
            break;
    }
}
