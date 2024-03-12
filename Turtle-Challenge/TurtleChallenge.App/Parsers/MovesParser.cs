using TurtleChallenge.App.Domain;
using TurtleChallenge.App.Enums;
using TurtleChallenge.App.Helpers;

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
                    var movement = move.ToMovementEnum();
                    resultMovesItem.Add(movement);
                }

                resultMoves.Add(resultMovesItem);
            }

            return new Moves(resultMoves);
        }
    }
}
