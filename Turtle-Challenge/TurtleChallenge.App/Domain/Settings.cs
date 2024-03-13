using TurtleChallenge.App.Enums;

namespace TurtleChallenge.App.Domain
{
    public sealed record Settings(Position BoardPosition, Position StartPointPosition, Position ExitPointPosition,
        Direction InitialDirection, IEnumerable<Position> MinesPosition);
}
