// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using TurtleChallenge.App.Applications;
using TurtleChallenge.App.Applications.Interfaces;
using TurtleChallenge.App.Helpers;
using TurtleChallenge.App.Helpers.Interfaces;

var settingsFile = args[0];
var movesFile = args[1];

var services = new ServiceCollection();
services.AddScoped<IFileWrapper, FileWrapper>();
services.AddScoped<IConsoleWrapper, ConsoleWrapper>();
services.AddScoped<ITurtleApplication, TurtleApplication>();
var sp = services.BuildServiceProvider();

var app = sp.GetRequiredService<ITurtleApplication>();
await app.RunAsync(settingsFile, movesFile);
