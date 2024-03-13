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

foreach (var (item, index) in moves?.Movements.Select((item, index) => (item, index)))
{
    var sequenceText = $"Sequence {index + 1}:";
    var initialDirection = settings.InitialDirection;
    var startPointPosition = new Position(settings.StartPointPosition);

    var turtle = new Turtle(initialDirection, startPointPosition);
    var result = turtleGame.Run(turtle, item);

    var resultTest = result switch
    {
        Result.Success => "Success!",
        Result.MineHit => "Mine hit!",
        Result.MovedOffBoard => "Moved off the board!",
        Result.MovesRanOut => "Still in danger!",
        _ => "Not implemented result",
    };

    Console.WriteLine($"{sequenceText} {resultTest}");
}
