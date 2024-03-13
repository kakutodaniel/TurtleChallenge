using TurtleChallenge.App.Domain;

namespace TurtleChallenge.App.Tests.Builders
{
    public class TurtleGameBuilder
    {
        private Settings _settings;
        
        public TurtleGameBuilder With(Settings settings)
        {
            _settings = settings;
            return this;
        }

        public TurtleGame Create()
        {
            return new TurtleGame(_settings);
        }
    }
}
