using System.Diagnostics;
using System.Runtime.InteropServices;

Console.WriteLine("Developed by Prajwal Aradhya");
Console.WriteLine("Github username: 07prajwal2000");
Console.WriteLine("This application makes Microsoft Teams status always Green/Available");

if (args.Length > 0 && args[0] == "help")
{
    var help = """
    * means required, () means optional and default value
    1st argument : Delay in milliseconds (4000 or 4sec)
    2nd argument : Process name to run. Check your Task manager to get the process name. (teams)

    Press any key to close. 🍻
    """;
    System.Console.WriteLine(help);
    Console.ReadKey();
    return;
}

[DllImport("user32.dll")]
static extern IntPtr SetForegroundWindow(IntPtr hWnds);
[DllImport("user32.dll")]
static extern bool ShowWindow(IntPtr hWnds, int nCmdShow);

var delay = 4000;

if (args.Length > 0 && int.TryParse(args[0], out var input_delay) && input_delay > delay)
{
    delay = input_delay;
}
var processName = args.Length > 1 ? args[1] : "";
if (string.IsNullOrWhiteSpace(processName))
{
    processName = "teams";
    Console.WriteLine("Process name is empty");
    Console.WriteLine("Fallback process: " + processName);
}
var runningProcesses = Process.GetProcesses().Where(x => x.ProcessName.Contains(processName, StringComparison.OrdinalIgnoreCase)).ToArray();

if (runningProcesses.Length == 0)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(processName + " must be running.");
    Console.ResetColor();
    Console.WriteLine("Press any key to close");
    Console.ReadKey();
    return;
}
var processWindowHandle = runningProcesses[0].MainWindowHandle;
var i = 0;
while (processWindowHandle == IntPtr.Zero && i < runningProcesses.Length)
{
    processWindowHandle = runningProcesses[i].MainWindowHandle;
    i++;
}
if (i == runningProcesses.Length)
{
    Console.WriteLine("No process found. exiting...");
    return;
}
var timestamp = TimeSpan.FromTicks(Stopwatch.GetTimestamp());
Console.WriteLine($"Delay: {delay}ms or {delay/1000}seconds");
Console.WriteLine($"Start time: {DateTime.Now.ToLongTimeString()}");
var counter = 0;
while (true)
{
    SetForegroundWindow(processWindowHandle);
    ShowWindow(processWindowHandle, 3);
    await Task.Delay(delay);
    ShowWindow(processWindowHandle, 6);
    await Task.Delay(delay);
    counter++;
    if (counter % 5 == 0)
    {
        var now = TimeSpan.FromTicks(Stopwatch.GetTimestamp()) - timestamp;
        Console.WriteLine($"TimeLog\nTime since startup: {now.Hours}H:{now.Minutes}M:{now.Seconds}S");
    }
}