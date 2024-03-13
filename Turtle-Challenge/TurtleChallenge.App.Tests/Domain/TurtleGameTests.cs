using TurtleChallenge.App.Domain;
using TurtleChallenge.App.Enums;
using TurtleChallenge.App.Tests.Builders;

namespace TurtleChallenge.App.Tests.Domain
{
    public class TurtleGameTests
    {
        [Fact]
        public void Constructor_WhenSettingsIsNull_ThrowsException()
        {
            // arrange & act
            var exception = Assert.Throws<ArgumentNullException>(() => new TurtleGameBuilder().Create());

            // assert
            Assert.Equal("Settings", exception.ParamName);
            Assert.Contains("Settings can not be null", exception.Message);
        }

        [Fact]
        public void Run_WhenTurtleDirectionNotEqualsInitialDirection_ThrowsException()
        {
            // arrange
            var position = new PositionBuilder()
                .WithAxisX(0)
                .WithAxisY(0)
                .Create();
            var turtle = new TurtleBuilder()
                .WithDirection(Enums.Direction.South)
                .WithPosition(position)
                .Create();
            var movements = new List<Movement>();
            var settings = BuildSettings();
            var turtleGame = new TurtleGame(settings);

            // act
            var exception = Assert.Throws<ArgumentException>(() => turtleGame.Run(turtle, movements));

            // arrange
            Assert.Equal("Direction", exception.ParamName);
            Assert.Contains("Invalid initial direction", exception.Message);
        }

        private Settings BuildSettings()
        {
            var boardPosition = new PositionBuilder()
                       .WithAxisX(5)
                       .WithAxisY(4)
                       .Create();

            var startPosition = new PositionBuilder()
                                .WithAxisX(0)
                                .WithAxisY(1)
                                .Create();

            var exitPosition = new PositionBuilder()
                                .WithAxisX(4)
                                .WithAxisY(2)
                                .Create();

            var minesPosition = new[]
            {
                new PositionBuilder().WithAxisX(0).WithAxisY(2).Create(),
                new PositionBuilder().WithAxisX(2).WithAxisY(0).Create(),
                new PositionBuilder().WithAxisX(2).WithAxisY(2).Create(),
                new PositionBuilder().WithAxisX(4).WithAxisY(3).Create(),
                new PositionBuilder().WithAxisX(4).WithAxisY(0).Create(),
            };

            return new SettingsBuilder()
                               .WithBoardPosition(boardPosition)
                               .WithStartPointPosition(startPosition)
                               .WithExitPointPosition(exitPosition)
                               .With(Enums.Direction.North)
                               .With(minesPosition)
                               .Create();
        }
    }
}
