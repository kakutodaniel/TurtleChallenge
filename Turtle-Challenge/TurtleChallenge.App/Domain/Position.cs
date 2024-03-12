namespace TurtleChallenge.App.Domain
{
    public class Position : IEquatable<Position>
    {
        public int AxisX { get; private set; }

        public int AxisY { get; private set; }

        public Position(int axisX, int axisY)
        {
            AxisX = axisX;
            AxisY = axisY;
        }

        public bool Equals(Position? other)
        {
            if (other == null)
            {
                return false;
            }

            return AxisX == other.AxisX && AxisY == other.AxisY;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Position);
        }

        public void AddAxisX(int val)
        {
            AxisX += val;
        }

        public void AddAxisY(int val)
        {
            AxisY += val;
        }
    }
}
