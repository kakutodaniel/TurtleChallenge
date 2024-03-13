using TurtleChallenge.App.Enums;
using TurtleChallenge.App.Errors;
using TurtleChallenge.App.Helpers;

namespace TurtleChallenge.App.Domain
{
    public sealed class Turtle
    {
        public Direction Direction { get; private set; }

        public Position Position { get; private set; }

        public Turtle(Direction direction, Position position)
        {
            Direction = direction;
            Position = position;

            Validate();
        }

        private void Validate()
        {
            if (Position is null)
            {
                throw new ArgumentNullException(nameof(Position), AppErrors.CanNotBeNull(nameof(Position)));
            }
        }

        public void Turn()
        {
            Direction = Direction.Turn();
        }

        public void Move()
        {
            var (addX, addY) = Direction switch
            {
                Direction.North => (0, -1),
                Direction.South => (0, 1),
                Direction.West => (-1, 0),
                Direction.East => (1, 0),
                _ => (0,0)
            };

            Position.AddAxisX(addX);
            Position.AddAxisY(addY);
        }
    }
}
