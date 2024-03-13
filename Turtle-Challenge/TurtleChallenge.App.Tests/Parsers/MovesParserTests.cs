using TurtleChallenge.App.Parsers;

namespace TurtleChallenge.App.Tests.Parsers
{
    public class MovesParserTests
    {
        [Fact]
        public void Parse_GivenMovesSequence_ReturnsMovesSucessfully()
        {
            // arrange
            var moves = new[] 
            { 
                "m,r,m,r,m,m,m,r,r,r,m,m,r,r,r,m,r,m,m",
                "m,r,m,r,m,r,r,r,m,m,m,r,m,r,r,r,m",
                "r,r,m"
            };

            // act
            var movesDomain = MovesParser.Parse(moves);

            // assert
            Assert.Equal(moves.Length, movesDomain.Movements.Count);
        }
    }
}
