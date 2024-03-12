using TurtleChallenge.App.Domain;

namespace TurtleChallenge.App.Tests.Builders
{
    public class PositionBuilder
    {
        private int _axisX;
        private int _axisY;
        private Position _position;

        public PositionBuilder WithAxisX(int axisX)
        {
            _axisX = axisX;
            return this;
        }

        public PositionBuilder WithAxisY(int axisY)
        {
            _axisY = axisY;
            return this;
        }

        public PositionBuilder With(Position position)
        {
            _position = position;
            return this;
        }

        public Position Create()
        {
            return new Position(_axisX, _axisY);
        }

        public Position Clone()
        {
            return new Position(_position);
        }
    }
}
