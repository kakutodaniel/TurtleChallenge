namespace TurtleChallenge.App.Errors
{
    public sealed class AppErrors
    {
        public static string InvalidSettings => "Invalid settings";

        public static string InvalidInitialDirection => "Invalid initial direction";

        public static string InvalidInitialPosition => "Invalid initial position";

        public static string InvalidDirection => "Invalid direction";

        public static string InvalidValue => "Invalid value";

        public static string InvalidMovement => "Invalid movement";

        public static string CanNotBeNull(params string[] args) => string.Format("{0} can not be null", args);
    }
}
