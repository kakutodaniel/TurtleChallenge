namespace TurtleChallenge.App.Helpers.Interfaces
{
    public interface IFileWrapper
    {
        bool Exists(string path);

        string ReadAllText(string path);

        string[] ReadAllLines(string path);
    }
}
