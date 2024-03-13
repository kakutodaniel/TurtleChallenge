using TurtleChallenge.App.Domain;
using TurtleChallenge.App.Enums;
using TurtleChallenge.App.Helpers.Interfaces;
using TurtleChallenge.App.Parsers;

namespace TurtleChallenge.App.Applications
{
    public sealed class TurtleApplication
    {
        private readonly string _settingsFile;

        private readonly string _movesFile;

        private readonly IFileWrapper _fileWrapper;

        private readonly IConsoleWrapper _consoleWrapper;

        public TurtleApplication(
            string settingsFile,
            string movesFile,
            IFileWrapper fileWrapper,
            IConsoleWrapper consoleWrapper)
        {
            _settingsFile = settingsFile;
            _movesFile = movesFile;
            _fileWrapper = fileWrapper;
            _consoleWrapper = consoleWrapper;
        }

        public void Run()
        {
            Settings settings = null;
            Moves moves = null;

            if (_fileWrapper.Exists(_settingsFile))
            {
                var settingsLine = _fileWrapper.ReadAllText(_settingsFile);

                settings = SettingsParser.Parse(settingsLine);
            }

            if (_fileWrapper.Exists(_movesFile))
            {
                var moveSequences = _fileWrapper.ReadAllLines(_movesFile).Where(x => !string.IsNullOrWhiteSpace(x));

                moves = MovesParser.Parse(moveSequences);
            }

            var turtleGame = new TurtleGame(settings);

            if (moves is null)
            {
                _consoleWrapper.WriteLine("Missing movements!");
                return;
            }

            foreach (var (item, index) in moves.Movements.Select((item, index) => (item, index)))
            {
                var sequenceText = $"Sequence {index + 1}:";
                var initialDirection = settings.InitialDirection;
                var startPointPosition = new Position(settings.StartPointPosition);

                var turtle = new Turtle(initialDirection, startPointPosition);
                var result = turtleGame.Run(turtle, item);

                var moveResult = result switch
                {
                    Result.Success => "Success!",
                    Result.MineHit => "Mine hit!",
                    Result.MovedOffBoard => "Moved off the board!",
                    Result.MovesRanOut => "Still in danger!",
                    _ => "Not implemented result",
                };

                _consoleWrapper.WriteLine($"{sequenceText} {moveResult}");
            }
        }
    }
}
