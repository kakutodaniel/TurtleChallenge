using TurtleChallenge.App.Domain;
using TurtleChallenge.App.Enums;

namespace TurtleChallenge.App.Parsers
{
    public class SettingsParser
    {
        public static Settings Parse(string settings)
        {
            var settingItems = settings.Split(';');
            var boardSize = settingItems[0].Split('x');
            var startingPoint = settingItems[1].Split(',');
            _ = Enum.TryParse<Direction>(settingItems[2], true, out var direction);
            var exitPoint = settingItems[3].Split(',');
            var minesHashSet = settingItems[4].Split('|').ToHashSet();

            if (!int.TryParse(boardSize[0], out var boardSizeAxisX))
            {
                throw new ArgumentException("Invalid board size");
            }

            if (!int.TryParse(boardSize[1], out var boardSizeAxisY))
            {
                throw new ArgumentException("Invalid board size");
            }

            if (!int.TryParse(startingPoint[0], out var startingPointAxisX))
            {
                throw new ArgumentException("Invalid starting point");
            }

            if (!int.TryParse(startingPoint[1], out var startingPointAxisY))
            {
                throw new ArgumentException("Invalid starting point");
            }

            if (!int.TryParse(exitPoint[0], out var exitPointAxisX))
            {
                throw new ArgumentException("Invalid exit point");
            }

            if (!int.TryParse(exitPoint[1], out var exitPointAxisY))
            {
                throw new ArgumentException("Invalid exit point");
            }

            return new Settings(
                boardSizeAxisX,
                boardSizeAxisY,
                startingPointAxisX,
                startingPointAxisY,
                direction,
                exitPointAxisX,
                exitPointAxisY,
                minesHashSet);
        }
    }
}
