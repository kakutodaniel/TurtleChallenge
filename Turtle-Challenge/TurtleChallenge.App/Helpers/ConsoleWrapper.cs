using TurtleChallenge.App.Helpers.Interfaces;

namespace TurtleChallenge.App.Helpers
{
    public class ConsoleWrapper : IConsoleWrapper
    {
        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }
    }
}
