using TurtleChallenge.App.Domain;
using TurtleChallenge.App.Enums;

namespace TurtleChallenge.App.Tests.Builders
{
    public class TurtleBuilder
    {
        private Direction _direction;
        private Position _position;
        
        public TurtleBuilder WithDirection(Direction direction)
        {
            _direction = direction;
            return this;
        }
        public TurtleBuilder WithPosition(Position position)
        {
            _position = position;
            return this;
        }

        public Turtle Create()
        {
            return new Turtle(_direction, _position);
        }
    }
}
