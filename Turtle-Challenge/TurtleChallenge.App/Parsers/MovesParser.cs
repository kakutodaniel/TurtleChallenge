using TurtleChallenge.App.Domain;
using TurtleChallenge.App.Enums;

namespace TurtleChallenge.App.Parsers
{
    public class MovesParser
    {
        public static Moves Parse(IEnumerable<string> movesSequence)
        {
            var resultMoves = new List<List<Movement>>();

            foreach (var moveLine in movesSequence)
            {
                var moves = moveLine.Split(',');
                var resultMovesItem = new List<Movement>();

                foreach (var move in moves)
                {
                    if (!Enum.TryParse<Movement>(move, true, out var result))
                    {
                        throw new ArgumentException("Invalid movement");
                    }

                    resultMovesItem.Add(result);
                }

                resultMoves.Add(resultMovesItem);
            }

            return new Moves(resultMoves);
        }
    }
}
