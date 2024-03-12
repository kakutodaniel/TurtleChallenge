using TurtleChallenge.App.Enums;
using TurtleChallenge.App.Helpers;

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
                throw new ArgumentException("Settings can not be null");
            }
        }

        public Result Run(Turtle turtle, List<Movement> moves)
        {
            foreach (var move in moves)
            {
                if (move == Movement.Rotate)
                {
                    UpdateDirection(turtle);
                    continue;
                }

                UpdateMovement(turtle);

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

        private void UpdateDirection(Turtle turtle)
        {
            var next = turtle.Direction.Next();
            turtle.UpdateDirection(next);
        }

        private void UpdateMovement(Turtle turtle)
        {
            switch (turtle.Direction)
            {
                case Direction.North:
                    turtle.Position.AddAxisY(-1);
                    break;
                case Direction.East:
                    turtle.Position.AddAxisX(1);
                    break;
                case Direction.South:
                    turtle.Position.AddAxisY(1);
                    break;
                case Direction.West:
                    turtle.Position.AddAxisX(-1);
                    break;
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
