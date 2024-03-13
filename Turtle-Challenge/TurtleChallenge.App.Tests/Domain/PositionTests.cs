using TurtleChallenge.App.Tests.Builders;

namespace TurtleChallenge.App.Tests.Domain
{
    public class PositionTests
    {
        [Fact]
        public void Equals_GivenEqualsEntity_ReturnsTrue()
        {
            // arrange
            var x = 1;
            var y = 2;
            var position1 = new PositionBuilder()
                            .WithAxisX(x)
                            .WithAxisY(y)
                            .Create();

            var position2 = new PositionBuilder()
                            .WithAxisX(x)
                            .WithAxisY(y)
                            .Create();

            // act
            var result = position1.Equals(position2);

            // assert
            Assert.True(result);
        }

        [Fact]
        public void AddAxisX_WhenAddValueToAxisX_AxisXIsUpdatedSuccessfully()
        {
            // arrange
            var x = 1;
            var y = 2;
            var increment = 10;
            var position = new PositionBuilder()
                            .WithAxisX(x)
                            .WithAxisY(y)
                            .Create();

            // act
            position.AddAxisX(increment);

            // assert
            Assert.Equal(y, position.AxisY);
            Assert.Equal(x + increment, position.AxisX);
        }

        [Fact]
        public void AddAxisY_WhenAddValueToAxisY_AxisYIsUpdatedSuccessfully()
        {
            // arrange
            var x = 1;
            var y = 2;
            var increment = 10;
            var position = new PositionBuilder()
                            .WithAxisX(x)
                            .WithAxisY(y)
                            .Create();

            // act
            position.AddAxisY(increment);

            // assert
            Assert.Equal(x, position.AxisX);
            Assert.Equal(y + increment, position.AxisY);
        }

        [Fact]
        public void Equals_GivenNullEntity_ReturnsFalse()
        {
            // arrange
            var position1 = new PositionBuilder()
                            .WithAxisX(1)
                            .WithAxisY(2)
                            .Create();

            // act
            var result = position1.Equals(null);

            // assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_GivenEntityWithDifferentValues_ReturnsFalse()
        {
            // arrange
            var position1 = new PositionBuilder()
                            .WithAxisX(1)
                            .WithAxisY(2)
                            .Create();

            var position2 = new PositionBuilder()
                            .WithAxisX(1)
                            .WithAxisY(3)
                            .Create();

            // act
            var result = position1.Equals(position2);

            // assert
            Assert.False(result);
        }

        [Fact]
        public void Constructor_GivenPositionAndCreateNewOne_ReturnsNewPositionSuccessfully()
        {
            // arrange
            var position = new PositionBuilder()
                            .WithAxisX(1)
                            .WithAxisY(2)
                            .Create();

            // act
            var newPosition = new PositionBuilder().Create(position);

            // arrange
            Assert.True(position != newPosition);
        }
    }
}
