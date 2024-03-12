using TurtleChallenge.App.Enums;

namespace TurtleChallenge.App.Domain
{
    public class Turtle
    {
        private Settings Settings;
        private Direction Direction;
        private int StartingPointAxisX;
        private int StartingPointAxisY;
        private string CurrentPoint;
        private readonly string ExitPoint;

        public Turtle(Settings settings)
        {
            Settings = settings;
            
            Validate();

            ExitPoint = $"{Settings.ExitPointAxisX},{Settings.ExitPointAxisY}";
        }

        private void Validate()
        {
            if (Settings is null)
            {
                throw new ArgumentException("Settings can not be null");
            }
        }

        public Result Run(List<Movement> moves)
        {
            Direction = Settings.InitialDirection;
            StartingPointAxisX = Settings.StartingPointAxisX;
            StartingPointAxisY = Settings.StartingPointAxisY;

            foreach (var move in moves)
            {
                if (move == Movement.R)
                {
                    UpdateDirection();
                    continue;
                }

                UpdateMovement();

                if (ReachedExit())
                {
                    return Result.Success;
                }

                if (HitMine())
                {
                    return Result.MineHit;
                }

                if (OffBoard())
                {
                    return Result.MovedOffBoard;
                }
            }

            return Result.MovesRanOut;
        }

        private void UpdateDirection()
        {
            Direction = Direction switch
            {
                var d when d is Direction.North or Direction.East or Direction.South =>
                                (Direction)(d.GetHashCode() + 1),
                var d when d is Direction.West => Direction.North
            };
        }

        private void UpdateMovement()
        {
            _ = Direction switch
            {
                var d when d is Direction.North => StartingPointAxisY--,
                var d when d is Direction.East => StartingPointAxisX++,
                var d when d is Direction.South => StartingPointAxisY++,
                var d when d is Direction.West => StartingPointAxisX--,
            };

            CurrentPoint = $"{StartingPointAxisX},{StartingPointAxisY}";
        }

        private bool HitMine()
        {
            return Settings.Mines.Contains(CurrentPoint);
        }

        private bool ReachedExit()
        {
            return CurrentPoint == ExitPoint;
        }

        private bool OffBoard()
        {
            if (StartingPointAxisX < 0 || StartingPointAxisY < 0)
            {
                return true;
            }

            if (StartingPointAxisX > Settings.BoardSizeAxisX - 1)
            {
                return true;
            }

            if (StartingPointAxisY > Settings.BoardSizeAxisY - 1)
            {
                return true;
            }

            return false;
        }
    }
}
