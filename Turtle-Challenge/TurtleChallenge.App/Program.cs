// See https://aka.ms/new-console-template for more information

using TurtleChallenge.App.Applications;
using TurtleChallenge.App.Helpers;

var settingsFile = args[0];
var movesFile = args[1];

var app = new TurtleApplication(settingsFile, movesFile, new FileWrapper(), new ConsoleWrapper());
app.Run();
