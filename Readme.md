# Make your Microsoft Teams always show Available/Green when you are not in front of the PC

# Updated
I updated the new code that uses just powershell script.
## Steps
- Download only 2 files
    - `Screen.ps1`
    - `run.bat`
- Store them in a single folder.
- MS Teams must be running (Old version, new version of teams doesn't work).
- Open the `run.bat` file.
- That's it 🎉. Go to sleep 😴, play games 🎮 or watch movies 🎬.

## Just run the application
(Make sure the MS Teams is running) or You can use any other running application which has a window, you can supply the process name (get the process name from the Task manager), provide the name as 2nd argument to the application using the command prompt or any shell you like.
```bash
ScreenActive.exe 4000 teams
# 1st argument - Delay amount 
# 2nd argument - The process name.
# OR use help command
ScreenActive.exe help
```

This application does nothing but Maximizes/Minimizes the provided process window within a given time interval. In this way it prevents the MS teams going away status. This trick works prefectly and I've used it personally.

## TLDR. WATCH THE TUTORIAL
https://github.com/07prajwal2000/Screen-Active/assets/54446909/fb7779e1-dd12-4b21-8255-8260ee93ab5f


## To download the Binary I have provided a zip file containing the executable and one bat file, you can either open bat or exe file, your choice. If you don't trust the binary you can always clone the repo and build it yourself using Dotnet 7.0

## CONSIDER GIVING A STAR 🌟.
