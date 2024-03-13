namespace TurtleChallenge.App.Applications.Interfaces
{
    public interface ITurtleApplication
    {
        Task RunAsync(string settingsFile, string movesFile);
    }
}
