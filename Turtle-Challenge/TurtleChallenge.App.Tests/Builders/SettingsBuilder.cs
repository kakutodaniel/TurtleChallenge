using TurtleChallenge.App.Domain;
using TurtleChallenge.App.Enums;

namespace TurtleChallenge.App.Tests.Builders
{
    public class SettingsBuilder
    {
        private Position _boardPosition;
        private Position _startPointPosition;
        private Position _exitPointPosition;
        private Direction _initialDirection;
        private IEnumerable<Position> _minesPosition;

        public SettingsBuilder WithBoardPosition(Position boardPosition)
        {
            _boardPosition = boardPosition;
            return this;
        }

        public SettingsBuilder WithStartPointPosition(Position startPointPosition)
        {
            _startPointPosition = startPointPosition;
            return this;
        }

        public SettingsBuilder WithExitPointPosition(Position exitPointPosition)
        {
            _exitPointPosition = exitPointPosition;
            return this;
        }

        public SettingsBuilder With(Direction initialDirection)
        {
            _initialDirection = initialDirection;
            return this;
        }

        public SettingsBuilder With(IEnumerable<Position> minesPosition)
        {
            _minesPosition = minesPosition;
            return this;
        }

        public Settings Create()
        {
            return new Settings(
                _boardPosition,
                _startPointPosition,
                _exitPointPosition,
                _initialDirection,
                _minesPosition);
        }
    }
}
