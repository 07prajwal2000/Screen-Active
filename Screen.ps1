$Source = @"
using System;
﻿using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

public class NoSleep {
    [DllImport("user32.dll")]
    static extern IntPtr SetForegroundWindow(IntPtr hWnds);
    [DllImport("user32.dll")]
    static extern bool ShowWindow(IntPtr hWnds, int nCmdShow);

    public NoSleep()
    {
        Run();
    }

    private void Run()
    {
        Console.WriteLine("Developed by Prajwal Aradhya");
        Console.WriteLine("Github username: 07prajwal2000");
        Console.WriteLine("This application makes Microsoft Teams status always Green/Available");
        Console.WriteLine("Started on: " + DateTime.Now);
        Process[] runningProcesses = Process.GetProcesses();

        Process teams = null;
        foreach(Process x in runningProcesses)
        {
            bool isTeams = x.ProcessName.Contains("teams") || x.ProcessName.Contains("Teams");
            if (isTeams && x.MainWindowHandle != IntPtr.Zero)
            {
                teams = x;
            }
        }

        if (teams == null)
        {
            Print("Teams not running. Press any key to close.");
            Console.ReadKey();
            return;
        }

        Console.CancelKeyPress += (s, e) => {
            Print("Closed");
            Environment.Exit(0);
        };
        Print("App Started");
        IntPtr windHandle = teams.MainWindowHandle;
        while(true)
        {
            Thread.Sleep(3000);
            SetForegroundWindow(windHandle);
            ShowWindow(windHandle, 3);
            Thread.Sleep(10000);
            ShowWindow(windHandle, 6);
            Thread.Sleep(10000);
        }

    }

    public void Print(object msg)
    {
        Console.WriteLine(msg);
    }
}
"@

Add-Type -TypeDefinition $Source
$BasicTestObject = New-Object NoSleep