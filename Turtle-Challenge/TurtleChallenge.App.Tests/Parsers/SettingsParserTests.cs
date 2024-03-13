using TurtleChallenge.App.Domain;
using TurtleChallenge.App.Enums;
using TurtleChallenge.App.Parsers;
using TurtleChallenge.App.Tests.Builders;

namespace TurtleChallenge.App.Tests.Parsers
{
    public class SettingsParserTests
    {
        [Fact]
        public void Parse_GivenSettingsWithInvalidSize_ThrowsException()
        {
            // arrange
            var settings = "1;2;3";

            // act
            var exception = Assert.Throws<ArgumentException>(() => SettingsParser.Parse(settings));

            // assert
            Assert.Equal("Settings", exception.ParamName, ignoreCase: true);
            Assert.Contains("Invalid settings", exception.Message);

        }

        [Fact]
        public void Parse_GivenValidSettings_ReturnsSettingsSuccessfully()
        {
            // arrange
            var boardExpected = new PositionBuilder().WithAxisX(5).WithAxisY(4).Create();
            var startPointExpected = new PositionBuilder().WithAxisX(0).WithAxisY(1).Create();
            var directionExpected = Direction.North;
            var exitPointExpected = new PositionBuilder().WithAxisX(4).WithAxisY(2).Create();
            var minesPositionExpected = new List<Position>
            {
                new PositionBuilder().WithAxisX(0).WithAxisY(2).Create(),
                new PositionBuilder().WithAxisX(2).WithAxisY(0).Create(),
                new PositionBuilder().WithAxisX(2).WithAxisY(2).Create(),
            };

            var settings = "5x4;0,1;north;4,2;0,2|2,0|2,2";

            // act
            var settingsDomain = SettingsParser.Parse(settings);

            // assert
            Assert.Equal(boardExpected, settingsDomain.BoardPosition);
            Assert.Equal(startPointExpected, settingsDomain.StartPointPosition);
            Assert.Equal(directionExpected, settingsDomain.InitialDirection);
            Assert.Equal(exitPointExpected, settingsDomain.ExitPointPosition);
            Assert.Equal(minesPositionExpected.Count(), settingsDomain.MinesPosition.Count());
            Assert.True(minesPositionExpected.SequenceEqual(settingsDomain.MinesPosition));
        }
    }
}
