using TurtleChallenge.App.Enums;

namespace TurtleChallenge.App.Helpers
{
    public static class Converters
    {
        public static Direction ToDirectionEnum(this string text)
        {
            if (!Enum.TryParse<Direction>(text, true, out var direction))
            {
                throw new ArgumentException("Invalid direction");
            }

            return direction;
        }

        public static int ToInt(this string text, string name)
        {
            if (!int.TryParse(text, out var @value))
            {
                throw new ArgumentException($"Invalid {name}");
            }

            return @value;
        }

        public static Movement ToMovementEnum(this string text)
        {
            var moveParsed = text.Equals("r", StringComparison.InvariantCultureIgnoreCase)
                                ? "rotate"
                                : text.Equals("m", StringComparison.InvariantCultureIgnoreCase)
                                ? "move"
                                : string.Empty;

            if (!Enum.TryParse<Movement>(moveParsed, true, out var movement))
            {
                throw new ArgumentException("Invalid movement");
            }

            return movement;
        }

        public static Direction Next(this Direction direction)
        {
            return direction switch
            {
                var d when d is Direction.North or Direction.East or Direction.South =>
                                (Direction)(d.GetHashCode() + 1),
                var d when d is Direction.West => Direction.North
            };
        }
    }
}
