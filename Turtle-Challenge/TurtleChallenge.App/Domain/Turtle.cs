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
        }

        public void UpdateDirection(Direction newDirection)
        {
            Direction = newDirection;
        }
    }
}
