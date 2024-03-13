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
        public void Run_GivenMovesEqualsNull_ThrowsException()
        {
            // arrange
            var position = new PositionBuilder()
                .WithAxisX(0)
                .WithAxisY(0)
                .Create();
            var turtle = new TurtleBuilder()
                .WithDirection(Direction.South)
                .WithPosition(position)
                .Create();

            var settings = BuildSettings();
            var turtleGame = new TurtleGame(settings);

            // act
            var exception = Assert.Throws<ArgumentNullException>(() => turtleGame.Run(turtle, null));

            // arrange
            Assert.Equal("Moves", exception.ParamName, ignoreCase: true);
            Assert.Contains("can not be null", exception.Message);
        }

        [Fact]
        public void Run_GivenTurtleEqualsNull_ThrowsException()
        {
            // arrange
            var movements = new List<Movement>();
            var settings = BuildSettings();
            var turtleGame = new TurtleGame(settings);

            // act
            var exception = Assert.Throws<ArgumentNullException>(() => turtleGame.Run(null, movements));

            // arrange
            Assert.Equal("Turtle", exception.ParamName, ignoreCase: true);
            Assert.Contains("can not be null", exception.Message);
        }

        [Fact]
        public void Run_GivenTurtleDirectionNotEqualsSettingsInitialDirection_ThrowsException()
        {
            // arrange
            var position = new PositionBuilder()
                .WithAxisX(0)
                .WithAxisY(0)
                .Create();
            var turtle = new TurtleBuilder()
                .WithDirection(Direction.South)
                .WithPosition(position)
                .Create();
            var movements = new List<Movement>();
            var settings = BuildSettings();
            var turtleGame = new TurtleGame(settings);

            // act
            var exception = Assert.Throws<ArgumentException>(() => turtleGame.Run(turtle, movements));

            // arrange
            Assert.Equal("Turtle", exception.ParamName, ignoreCase: true);
            Assert.Contains("Invalid initial direction", exception.Message);
        }

        [Fact]
        public void Run_GivenTurtleStartPositionNotEqualsSettingsStartPosition_ThrowsException()
        {
            // arrange
            var position = new PositionBuilder()
                .WithAxisX(0)
                .WithAxisY(0)
                .Create();
            var turtle = new TurtleBuilder()
                .WithDirection(Direction.North)
                .WithPosition(position)
                .Create();
            var movements = new List<Movement>();
            var settings = BuildSettings();
            var turtleGame = new TurtleGame(settings);

            // act
            var exception = Assert.Throws<ArgumentException>(() => turtleGame.Run(turtle, movements));

            // arrange
            Assert.Equal("Turtle", exception.ParamName, ignoreCase: true);
            Assert.Contains("Invalid initial position", exception.Message);
        }

        [Fact]
        public void Run_GivenTurtleAndMovementsWithRotates_ReturnsMovesRanOutSuccessfully()
        {
            // arrange
            var position = new PositionBuilder()
                .WithAxisX(0)
                .WithAxisY(1)
                .Create();
            var turtle = new TurtleBuilder()
                .WithDirection(Direction.North)
                .WithPosition(position)
                .Create();
            var movements = new List<Movement> { Movement.Rotate, Movement.Rotate };
            var settings = BuildSettings();
            var turtleGame = new TurtleGame(settings);

            // act
            var result = turtleGame.Run(turtle, movements);

            // arrange
            Assert.Equal(Result.MovesRanOut, result);
            Assert.Equal(Direction.South, turtle.Direction);
        }

        [Fact]
        public void Run_GivenTurtleAndMovementsWithRotatesAndMoves_WhenReachedExit_ReturnsSuccessfully()
        {
            // arrange
            var position = new PositionBuilder()
                .WithAxisX(0)
                .WithAxisY(1)
                .Create();
            var turtle = new TurtleBuilder()
                .WithDirection(Direction.North)
                .WithPosition(position)
                .Create();
            var movements = new List<Movement>
            {
                Movement.Rotate,
                Movement.Move,
                Movement.Move,
                Movement.Move,
                Movement.Move,
                Movement.Rotate,
                Movement.Move,
            };
            var settings = BuildSettings();
            var turtleGame = new TurtleGame(settings);

            // act
            var result = turtleGame.Run(turtle, movements);

            // arrange
            Assert.Equal(Result.Success, result);
            Assert.Equal(Direction.South, turtle.Direction);
        }

        [Fact]
        public void Run_GivenTurtleAndMovementsWithRotatesAndMoves_WhenHitMine_ReturnsMineHitSuccessfully()
        {
            // arrange
            var position = new PositionBuilder()
                .WithAxisX(0)
                .WithAxisY(1)
                .Create();
            var turtle = new TurtleBuilder()
                .WithDirection(Direction.North)
                .WithPosition(position)
                .Create();
            var movements = new List<Movement>
            {
                Movement.Move,
                Movement.Rotate,
                Movement.Move,
                Movement.Move,
            };
            var settings = BuildSettings();
            var turtleGame = new TurtleGame(settings);

            // act
            var result = turtleGame.Run(turtle, movements);

            // arrange
            Assert.Equal(Result.MineHit, result);
            Assert.Equal(Direction.East, turtle.Direction);
        }

        [Fact]
        public void Run_GivenTurtleAndMovementsWithRotatesAndMoves_WhenPositionAxisYNegative_ReturnsMovedOffSuccessfully()
        {
            // arrange
            var position = new PositionBuilder()
                .WithAxisX(0)
                .WithAxisY(1)
                .Create();
            var turtle = new TurtleBuilder()
                .WithDirection(Direction.North)
                .WithPosition(position)
                .Create();
            var movements = new List<Movement>
            {
                Movement.Move,
                Movement.Move,
            };
            var settings = BuildSettings();
            var turtleGame = new TurtleGame(settings);

            // act
            var result = turtleGame.Run(turtle, movements);

            // arrange
            Assert.Equal(Result.MovedOffBoard, result);
            Assert.Equal(Direction.North, turtle.Direction);
        }

        [Fact]
        public void Run_GivenTurtleAndMovementsWithRotatesAndMoves_WhenPositionAxisXNegative_ReturnsMovedOffSuccessfully()
        {
            // arrange
            var position = new PositionBuilder()
                .WithAxisX(0)
                .WithAxisY(1)
                .Create();
            var turtle = new TurtleBuilder()
                .WithDirection(Direction.North)
                .WithPosition(position)
                .Create();
            var movements = new List<Movement>
            {
                Movement.Rotate,
                Movement.Rotate,
                Movement.Rotate,
                Movement.Move,
            };
            var settings = BuildSettings();
            var turtleGame = new TurtleGame(settings);

            // act
            var result = turtleGame.Run(turtle, movements);

            // arrange
            Assert.Equal(Result.MovedOffBoard, result);
            Assert.Equal(Direction.West, turtle.Direction);
        }

        [Fact]
        public void Run_GivenTurtleAndMovementsWithRotatesAndMoves_WhenPositionAxisXGreaterThanBoard_ReturnsMovedOffSuccessfully()
        {
            // arrange
            var position = new PositionBuilder()
                .WithAxisX(0)
                .WithAxisY(1)
                .Create();
            var turtle = new TurtleBuilder()
                .WithDirection(Direction.North)
                .WithPosition(position)
                .Create();
            var movements = new List<Movement>
            {
                Movement.Rotate,
                Movement.Move,
                Movement.Move,
                Movement.Move,
                Movement.Move,
                Movement.Move,
            };
            var settings = BuildSettings();
            var turtleGame = new TurtleGame(settings);

            // act
            var result = turtleGame.Run(turtle, movements);

            // arrange
            Assert.Equal(Result.MovedOffBoard, result);
            Assert.Equal(Direction.East, turtle.Direction);
        }

        [Fact]
        public void Run_GivenTurtleAndMovementsWithRotatesAndMoves_WhenPositionAxisYGreaterThanBoard_ReturnsMovedOffSuccessfully()
        {
            // arrange
            var position = new PositionBuilder()
                .WithAxisX(0)
                .WithAxisY(1)
                .Create();
            var turtle = new TurtleBuilder()
                .WithDirection(Direction.North)
                .WithPosition(position)
                .Create();
            var movements = new List<Movement>
            {
                Movement.Rotate,
                Movement.Rotate,
                Movement.Move,
                Movement.Move,
                Movement.Move,
            };
            var settings = BuildSettings();
            var turtleGame = new TurtleGame(settings);

            // act
            var result = turtleGame.Run(turtle, movements);

            // arrange
            Assert.Equal(Result.MovedOffBoard, result);
            Assert.Equal(Direction.South, turtle.Direction);
        }

        private Settings BuildSettings(
            Position boardPosition = null,
            Position startPosition = null,
            Position exitPosition = null,
            Position[] minesPosition = null,
            Direction initialDirection = Direction.North)
        {
            boardPosition ??= new PositionBuilder()
                                       .WithAxisX(5)
                                       .WithAxisY(4)
                                       .Create();

            startPosition ??= new PositionBuilder()
                                .WithAxisX(0)
                                .WithAxisY(1)
                                .Create();

            exitPosition ??= new PositionBuilder()
                                .WithAxisX(4)
                                .WithAxisY(2)
                                .Create();

            minesPosition ??= new[]
            {
                new PositionBuilder().WithAxisX(2).WithAxisY(0).Create(),
                new PositionBuilder().WithAxisX(2).WithAxisY(2).Create(),
                new PositionBuilder().WithAxisX(4).WithAxisY(3).Create(),
                new PositionBuilder().WithAxisX(4).WithAxisY(0).Create(),
            };

            return new SettingsBuilder()
                               .WithBoardPosition(boardPosition)
                               .WithStartPointPosition(startPosition)
                               .WithExitPointPosition(exitPosition)
                               .With(initialDirection)
                               .With(minesPosition)
                               .Create();
        }
    }
}
