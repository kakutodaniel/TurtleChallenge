using TurtleChallenge.App.Enums;

namespace TurtleChallenge.App.Domain
{
    public class Turtle
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
                throw new ArgumentNullException(nameof(Position));
            }
        }

        public void UpdateDirection(Direction newDirection)
        {
            Direction = newDirection;
        }
    }
}
