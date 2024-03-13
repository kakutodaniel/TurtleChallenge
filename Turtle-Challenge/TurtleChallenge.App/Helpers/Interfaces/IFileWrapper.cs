namespace TurtleChallenge.App.Helpers.Interfaces
{
    public interface IFileWrapper
    {
        bool Exists(string path);

        Task<string> ReadAllTextAsync(string path);

        Task<string[]> ReadAllLinesAsync(string path);
    }
}
