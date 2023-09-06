using System.Diagnostics;
using System.Runtime.InteropServices;

System.Console.WriteLine("Developed by Prajwal Aradhya");
System.Console.WriteLine("Github username: 07prajwal2000");
System.Console.WriteLine("This application makes Microsoft Teams status always Green/Available");

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
}

[DllImport("user32.dll")]
static extern IntPtr SetForegroundWindow(IntPtr hWnds);
[DllImport("user32.dll")]
static extern bool ShowWindow(IntPtr hWnds, int nCmdShow);

var delay = 4000;

if (args.Length > 0 && int.TryParse(args[0], out var d) && d > delay) {
    delay = d;
}
var processName = args.Length > 1 ? args[1] : "";
if (string.IsNullOrWhiteSpace(processName))
{
    processName = "teams";
    System.Console.WriteLine("Provided Process name is empty. Choosing the default process 'teams' application.");
    System.Console.WriteLine("Fallback process: " + processName);
}
var teams = Process.GetProcesses().Where(x => x.ProcessName.Contains(processName, StringComparison.OrdinalIgnoreCase)).ToArray();

if (teams.Length == 0)
{
    Console.ForegroundColor = ConsoleColor.Red;
    System.Console.WriteLine(processName + " must be running.");
    Console.ResetColor();
    System.Console.WriteLine("Press any key to close");
    Console.ReadKey();
    return;
}
var teamsHandle = teams[0].MainWindowHandle;
var i = 0;
while (teamsHandle == IntPtr.Zero && i < teams.Length)
{
    teamsHandle = teams[i].MainWindowHandle;
    i++;
}
if (i == teams.Length) 
{
    System.Console.WriteLine("No process found. exiting...");
    return;
}
var timestamp = TimeSpan.FromTicks(Stopwatch.GetTimestamp());
Console.WriteLine($"Start time: {DateTime.Now.ToLongTimeString()}");
var counter = 0;
while (true)
{
    SetForegroundWindow(teamsHandle);
    ShowWindow(teamsHandle, 3);
    await Task.Delay(delay);
    ShowWindow(teamsHandle, 6);
    await Task.Delay(delay);
    counter++;
    if (counter % 5 == 0)
    {
        var now = TimeSpan.FromTicks(Stopwatch.GetTimestamp()) - timestamp;
        System.Console.WriteLine($"TimeLog\nTime since startup: {now.Hours}H:{now.Minutes}M:{now.Seconds}S");
    }
}

// IGNORE from here
IntPtr GetSelectedProcess()
{
    var processes = Process.GetProcesses().Where(x => x.MainWindowHandle != IntPtr.Zero && !string.IsNullOrEmpty(x.MainWindowTitle)).ToArray();
    System.Console.WriteLine("Use arrow keys to move through the list of process");
    var starting = Console.CursorTop;
    foreach (var p in processes)
    {
        System.Console.WriteLine("    {0}", p.MainWindowTitle);
    }
    var ending = starting + processes.Length;
    var i = 0;
    var keyInfo = Console.ReadKey();
    while (keyInfo.Key != ConsoleKey.Enter)
    {
        if (keyInfo.Key == ConsoleKey.UpArrow)
        {
            i--;
            if (i < 0)
            {
                i = processes.Length - 1;
            }
            i %= processes.Length;
        } else if (keyInfo.Key == ConsoleKey.DownArrow)
        {
            i++;
            if (i > processes.Length)
            {
                i = 0;
            }
            i %= processes.Length;
        }
        keyInfo = Console.ReadKey();
        var cur = starting + i;
        Console.SetCursorPosition(0, cur);
        Console.Write(">");
    }
    Console.SetCursorPosition(0, ending + 1);
    System.Console.WriteLine("Selected process " + processes[i].MainWindowTitle);
    return processes[i].MainWindowHandle;
}