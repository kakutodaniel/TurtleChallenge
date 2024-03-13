using TurtleChallenge.App.Enums;

namespace TurtleChallenge.App.Domain
{
    public class TurtleGame
    {
        private readonly Settings Settings;

        public TurtleGame(Settings settings)
        {
            Settings = settings;
            Validate();
        }

        private void Validate()
        {
            if (Settings is null)
            {
                throw new ArgumentNullException(nameof(Settings), "Settings can not be null");
            }
        }

        public Result Run(Turtle turtle, List<Movement> moves)
        {
            ValidateRun(turtle);

            foreach (var move in moves)
            {
                if (move == Movement.Rotate)
                {
                    turtle.Turn();
                    continue;
                }

                turtle.Move();

                if (ReachedExit(turtle))
                {
                    return Result.Success;
                }

                if (HitMine(turtle))
                {
                    return Result.MineHit;
                }

                if (OffBoard(turtle))
                {
                    return Result.MovedOffBoard;
                }
            }

            return Result.MovesRanOut;
        }

        private void ValidateRun(Turtle turtle)
        {
            if (turtle.Direction != Settings.InitialDirection)
            {
                throw new ArgumentException("Invalid initial direction", nameof(turtle.Direction));
            }

            if (!turtle.Position.Equals(Settings.StartPointPosition))
            {
                throw new ArgumentException("Invalid initial position", nameof(turtle.Position));
            }
        }

        private bool HitMine(Turtle turtle)
        {
            return Settings.MinesPosition.Contains(turtle.Position);
        }

        private bool ReachedExit(Turtle turtle)
        {
            return turtle.Position.Equals(Settings.ExitPointPosition);
        }

        private bool OffBoard(Turtle turtle)
        {
            if (turtle.Position.AxisX < 0 || turtle.Position.AxisY < 0)
            {
                return true;
            }

            if (turtle.Position.AxisX > Settings.BoardPosition.AxisX - 1)
            {
                return true;
            }

            if (turtle.Position.AxisY > Settings.BoardPosition.AxisY - 1)
            {
                return true;
            }

            return false;
        }
    }
}
