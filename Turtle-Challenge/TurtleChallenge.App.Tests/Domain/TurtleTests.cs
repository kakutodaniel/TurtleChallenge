using TurtleChallenge.App.Enums;
using TurtleChallenge.App.Tests.Builders;

namespace TurtleChallenge.App.Tests.Domain
{
    public class TurtleTests
    {
        [Fact]
        public void Turn_GivenDirection_TurnsToNextDirectionSuccessfully()
        {
            // arrange
            var position = new PositionBuilder().Create();
            var turtle = new TurtleBuilder()
                                .WithDirection(Direction.North)
                                .WithPosition(position)
                                .Create();

            // act
            turtle.Turn();

            // assert
            Assert.Equal(Direction.East, turtle.Direction);
        }

        [Fact]
        public void Move_GivenPositionAndNorthDirection_AddAxisYSuccessfully()
        {
            // arrange
            var position = new PositionBuilder().Create();
            var turtle = new TurtleBuilder()
                                .WithDirection(Direction.North)
                                .WithPosition(position)
                                .Create();

            var vl = position.AxisY - 1;

            // act
            turtle.Move();

            // assert
            Assert.Equal(Direction.North, turtle.Direction);
            Assert.Equal(vl, turtle.Position.AxisY);
        }

        [Fact]
        public void Move_GivenPositionAndSouthDirection_AddAxisYSuccessfully()
        {
            // arrange
            var position = new PositionBuilder().Create();
            var turtle = new TurtleBuilder()
                                .WithDirection(Direction.South)
                                .WithPosition(position)
                                .Create();

            var vl = position.AxisY + 1;

            // act
            turtle.Move();

            // assert
            Assert.Equal(Direction.South, turtle.Direction);
            Assert.Equal(vl, turtle.Position.AxisY);
        }

        [Fact]
        public void Move_GivenPositionAndEastDirection_AddAxisXSuccessfully()
        {
            // arrange
            var position = new PositionBuilder().Create();
            var turtle = new TurtleBuilder()
                                .WithDirection(Direction.East)
                                .WithPosition(position)
                                .Create();

            var vl = position.AxisX + 1;

            // act
            turtle.Move();

            // assert
            Assert.Equal(Direction.East, turtle.Direction);
            Assert.Equal(vl, turtle.Position.AxisX);
        }

        [Fact]
        public void Move_GivenPositionAndWestDirection_AddAxisXSuccessfully()
        {
            // arrange
            var position = new PositionBuilder().Create();
            var turtle = new TurtleBuilder()
                                .WithDirection(Direction.West)
                                .WithPosition(position)
                                .Create();

            var vl = position.AxisX - 1;

            // act
            turtle.Move();

            // assert
            Assert.Equal(Direction.West, turtle.Direction);
            Assert.Equal(vl, turtle.Position.AxisX);
        }

        [Fact]
        public void Constructor_WhenPositionIsNull_ThrowsException()
        {
            // arrange & act
            var exception = Assert.Throws<ArgumentNullException>(() => new TurtleBuilder().Create());

            // assert
            Assert.Equal("Position", exception.ParamName);
            Assert.Contains("Position can not be null", exception.Message);
        }
    }
}
