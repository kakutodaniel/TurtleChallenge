using TurtleChallenge.App.Domain;
using TurtleChallenge.App.Helpers;

namespace TurtleChallenge.App.Parsers
{
    public class SettingsParser
    {
        public static Settings Parse(string settings)
        {
            var settingItems = settings.Split(';');
            var boardSize = settingItems[0].Split('x');
            var startingPoint = settingItems[1].Split(',');
            var direction = settingItems[2].ToDirectionEnum();
            var exitPoint = settingItems[3].Split(',');

            var minesPosition = CreateMinesPosition(settingItems[4]);

            var boardSizeAxisX = boardSize[0].ToInt("boardSizeAxisX");
            var boardSizeAxisY = boardSize[1].ToInt("boardSizeAxisY");
            var startingPointAxisX = startingPoint[0].ToInt("startingPointAxisX");
            var startingPointAxisY = startingPoint[1].ToInt("startingPointAxisY");
            var exitPointAxisX = exitPoint[0].ToInt("exitPointAxisX");
            var exitPointAxisY = exitPoint[1].ToInt("exitPointAxisY");

            var boardPosition = new Position(boardSizeAxisX, boardSizeAxisY);
            var startPointPosition = new Position(startingPointAxisX, startingPointAxisY);
            var exitPointPosition = new Position(exitPointAxisX, exitPointAxisY);

            return new Settings(
                boardPosition,
                startPointPosition,
                exitPointPosition,
                direction,
                minesPosition);
        }

        private static IEnumerable<Position> CreateMinesPosition(string minesString)
        {
            var minesCollection = minesString.Split('|');
            var lstResult = new List<Position>();

            foreach (var item in minesCollection)
            {
                var mine = item.Split(',');

                var mineAxisX = mine[0].ToInt("mineAxisX");
                var mineAxisY = mine[1].ToInt("mineAxisY");

                lstResult.Add(new Position(mineAxisX, mineAxisY));
            }

            return lstResult;
        }
    }
}
