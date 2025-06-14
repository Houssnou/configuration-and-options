// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

var builder =
    Host.CreateEmptyApplicationBuilder(null);

builder.Configuration.AddJsonFile(path: "appsettings.json", optional: true, reloadOnChange: true);
builder.Configuration.AddJsonFile(path: $"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

Console.WriteLine(builder.Configuration.GetDebugView());

// Watch for changes to appsettings.json
using var watcher = new FileSystemWatcher(Directory.GetCurrentDirectory(), "appsettings.json");
watcher.NotifyFilter = NotifyFilters.LastWrite;
watcher.Changed += (s, e) =>
{
    // Wait briefly to ensure file write is complete
    System.Threading.Thread.Sleep(100);
    Console.WriteLine("Configuration changed:");
    Console.WriteLine(builder.Configuration.GetDebugView());
};
watcher.EnableRaisingEvents = true;

// Keep the app running
Console.WriteLine("Watching for configuration changes. Press Enter to exit.");
Console.ReadLine();
