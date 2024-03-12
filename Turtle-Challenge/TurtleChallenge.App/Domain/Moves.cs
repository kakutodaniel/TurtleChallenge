using TurtleChallenge.App.Enums;

namespace TurtleChallenge.App.Domain
{
    public class Moves
    {
        public List<List<Movement>> Movements { get; private set; }

        public Moves(List<List<Movement>> movements)
        {
            Movements = movements;
        }
    }
}
