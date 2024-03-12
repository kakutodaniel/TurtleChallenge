using TurtleChallenge.App.Enums;
using TurtleChallenge.App.Tests.Builders;

namespace TurtleChallenge.App.Tests.Domain
{
    public class TurtleTests
    {
        [Fact]
        public void UpdateDirection_GivenValidTurtle_DirectionUpdatedSuccessfully()
        {
            // arrange
            var position = new PositionBuilder().Create();
            var turtle = new TurtleBuilder()
                                .WithDirection(Direction.North)
                                .WithPosition(position)
                                .Create();

            // act
            turtle.UpdateDirection(Direction.South);

            // assert
            Assert.Equal(Direction.South, turtle.Direction);
            Assert.Equal(position.AxisX, turtle.Position.AxisX);
            Assert.Equal(position.AxisY, turtle.Position.AxisY);
        }

        [Fact]
        public void Constructor_WhenPositionIsNull_ThrowsException()
        {
            // arrange & act
            var exception = Assert.Throws<ArgumentNullException>(() => new TurtleBuilder().Create());

            // assert
            Assert.Equal("Position", exception.ParamName);
        }
    }
}
