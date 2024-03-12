using TurtleChallenge.App.Enums;

namespace TurtleChallenge.App.Domain
{
    public class Settings
    {
        public Settings(
            int boardSizeAxisX,
            int boardSizeAxisY,
            int startingPointAxisX,
            int startingPointAxisY,
            Direction initialDirection,
            int exitPointAxisX,
            int exitPointAxisY,
            HashSet<string> mines)
        {
            BoardSizeAxisX = boardSizeAxisX;
            BoardSizeAxisY = boardSizeAxisY;
            StartingPointAxisX = startingPointAxisX;
            StartingPointAxisY = startingPointAxisY;
            InitialDirection = initialDirection;
            ExitPointAxisX = exitPointAxisX;
            ExitPointAxisY = exitPointAxisY;
            Mines = mines;
        }

        public int BoardSizeAxisX { get; private set; }

        public int BoardSizeAxisY { get; private set; }

        public int StartingPointAxisX { get; private set; }

        public int StartingPointAxisY { get; private set; }

        public Direction InitialDirection { get; private set; }

        public int ExitPointAxisX { get; private set; }

        public int ExitPointAxisY { get; private set; }

        public HashSet<string> Mines { get; private set; }
    }
}
