using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleChallenge.App.Enums;
using TurtleChallenge.App.Helpers;

namespace TurtleChallenge.App.Tests.Helpers
{
    public class ConvertersTests
    {
        [Fact]
        public void ToDirectionEnum_GivenNonExistentDirection_ThrowsException()
        {
            // arrange & act
            var exception = Assert.Throws<ArgumentException>(() => Converters.ToDirectionEnum("random"));

            // assert
            Assert.Equal("Invalid direction", exception.Message);
        }

        [Theory]
        [InlineData("nOrTh", Direction.North)]
        [InlineData("eASt", Direction.East)]
        [InlineData("south", Direction.South)]
        [InlineData("wEst", Direction.West)]
        public void ToDirectionEnum_GivenExistentDirection_ReturnsDirection(string text, Direction expected)
        {
            // arrange & act
            var direction = Converters.ToDirectionEnum(text);

            // assert
            Assert.Equal(expected, direction);
        }

        [Fact]
        public void ToInt_GivenInvalidInt_ThrowsException()
        {
            // arrange & act
            var text = "toint";
            var name = "random";
            var exception = Assert.Throws<ArgumentException>(() => Converters.ToInt(text, name));

            // assert
            Assert.Equal(name, exception.ParamName);
            Assert.Contains($"Invalid value", exception.Message);
        }

        [Fact]
        public void ToInt_GivenValidInt_ReturnsInt()
        {
            // arrange & act
            var text = "200";
            var name = "random";

            // act
            var result = Converters.ToInt(text, name);

            // assert
            Assert.IsType<int>(result);
        }

        [Fact]
        public void ToMovementEnum_GivenNonExistentMovement_ThrowsException()
        {
            // arrange & act
            var exception = Assert.Throws<ArgumentException>(() => Converters.ToMovementEnum("random"));

            // assert
            Assert.Equal("Invalid movement", exception.Message);
        }

        [Theory]
        [InlineData("r", Movement.Rotate)]
        [InlineData("R", Movement.Rotate)]
        [InlineData("m", Movement.Move)]
        [InlineData("M", Movement.Move)]
        public void ToMovementEnum_GivenExistentMovement_ReturnsMovement(string text, Movement expected)
        {
            // arrange & act
            var direction = Converters.ToMovementEnum(text);

            // assert
            Assert.Equal(expected, direction);
        }

        [Theory]
        [InlineData(Direction.North, Direction.East)]
        [InlineData(Direction.East, Direction.South)]
        [InlineData(Direction.South, Direction.West)]
        [InlineData(Direction.West, Direction.North)]
        public void Next_GivenDirection_ReturnsNextDirection(Direction direction, Direction expected)
        {
            // arrange & act
            var result = direction.Next();

            // assert
            Assert.Equal(expected, result);
        }
    }
}
