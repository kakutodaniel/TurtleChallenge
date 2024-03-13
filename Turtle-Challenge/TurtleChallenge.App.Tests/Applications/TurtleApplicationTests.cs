using Moq;
using TurtleChallenge.App.Applications;
using TurtleChallenge.App.Helpers.Interfaces;

namespace TurtleChallenge.App.Tests.Applications
{
    public class TurtleApplicationTests
    {
        private Mock<IFileWrapper> _fileWrapperMock;
        private Mock<IConsoleWrapper> _consoleWrapperMock;
        private TurtleApplication _app;
        private const string _settingsFile = "settingsFile";
        private const string _movesFile = "movesFile";

        public TurtleApplicationTests()
        {
            _fileWrapperMock = new Mock<IFileWrapper>();
            _consoleWrapperMock = new Mock<IConsoleWrapper>();
            _app = new TurtleApplication(
                                _settingsFile,
                                _movesFile,
                                _fileWrapperMock.Object,
                                _consoleWrapperMock.Object);
        }

        [Fact]
        public void Run_GivenSettingsFileAndNotExistentMovesFiles_ReturnsSuccessfullyAndWriteMissingMovements()
        {
            // arrange
            string text = "";
            _fileWrapperMock.Setup(x => x.Exists(_settingsFile)).Returns(true);
            _fileWrapperMock.Setup(x => x.ReadAllText(_settingsFile)).Returns("5x4;0,1;north;4,2;0,2|2,0|2,2|4,3|4,0");
            _fileWrapperMock.Setup(x => x.Exists(_movesFile)).Returns(false);

            _consoleWrapperMock.Setup(x => x.WriteLine(It.IsAny<string>()))
                .Callback<string>((msg) => text = msg);

            // act
            _app.Run();

            // assert
            Assert.Equal("Missing movements!", text);
        }

        [Fact]
        public void Run_GivenSettingsFileAndMovesFiles_ReturnsSuccessfullyAndWriteMovesRanOut()
        {
            // arrange
            string text = "";
            _fileWrapperMock.Setup(x => x.Exists(_settingsFile)).Returns(true);
            _fileWrapperMock.Setup(x => x.ReadAllText(_settingsFile)).Returns("5x4;0,1;north;4,2;0,2|2,0|2,2|4,3|4,0");
            _fileWrapperMock.Setup(x => x.Exists(_movesFile)).Returns(true);
            _fileWrapperMock.Setup(x => x.ReadAllLines(_movesFile)).Returns(new[] { "r,m,m,m,m,r" });

            _consoleWrapperMock.Setup(x => x.WriteLine(It.IsAny<string>()))
                    .Callback<string>((msg) => text = msg);

            // act
            _app.Run();

            // assert
            Assert.Contains("Still in danger!", text);
        }

        [Fact]
        public void Run_GivenSettingsFileAndMovesFiles_ReturnsSuccessfullyAndWriteSuccess()
        {
            // arrange
            string text = "";
            _fileWrapperMock.Setup(x => x.Exists(_settingsFile)).Returns(true);
            _fileWrapperMock.Setup(x => x.ReadAllText(_settingsFile)).Returns("5x4;0,1;north;4,2;0,2|2,0|2,2|4,3|4,0");
            _fileWrapperMock.Setup(x => x.Exists(_movesFile)).Returns(true);
            _fileWrapperMock.Setup(x => x.ReadAllLines(_movesFile)).Returns(new[] { "r,m,m,m,m,r,m,r,r,r,m" });

            _consoleWrapperMock.Setup(x => x.WriteLine(It.IsAny<string>()))
                    .Callback<string>((msg) => text = msg);

            // act
            _app.Run();

            // assert
            Assert.Contains("Success!", text);
        }

        [Fact]
        public void Run_GivenSettingsFileAndMovesFiles_ReturnsSuccessfullyAndWriteMineHit()
        {
            // arrange
            string text = "";
            _fileWrapperMock.Setup(x => x.Exists(_settingsFile)).Returns(true);
            _fileWrapperMock.Setup(x => x.ReadAllText(_settingsFile)).Returns("5x4;0,1;north;4,2;0,2|2,0|2,2|4,3|4,0");
            _fileWrapperMock.Setup(x => x.Exists(_movesFile)).Returns(true);
            _fileWrapperMock.Setup(x => x.ReadAllLines(_movesFile)).Returns(new[] { "m,r,m,r,m,m,m,r,m,r,m" });

            _consoleWrapperMock.Setup(x => x.WriteLine(It.IsAny<string>()))
                    .Callback<string>((msg) => text = msg);

            // act
            _app.Run();

            // assert
            Assert.Contains("Mine hit!", text);
        }

        [Fact]
        public void Run_GivenSettingsFileAndMovesFiles_ReturnsSuccessfullyAndWriteMovedOffTheBoard()
        {
            // arrange
            string text = "";
            _fileWrapperMock.Setup(x => x.Exists(_settingsFile)).Returns(true);
            _fileWrapperMock.Setup(x => x.ReadAllText(_settingsFile)).Returns("5x4;0,1;north;4,2;0,2|2,0|2,2|4,3|4,0");
            _fileWrapperMock.Setup(x => x.Exists(_movesFile)).Returns(true);
            _fileWrapperMock.Setup(x => x.ReadAllLines(_movesFile)).Returns(new[] { "r,m,r,m,m,m" });

            _consoleWrapperMock.Setup(x => x.WriteLine(It.IsAny<string>()))
                    .Callback<string>((msg) => text = msg);

            // act
            _app.Run();

            // assert
            Assert.Contains("Moved off the board!", text);
        }
    }
}
