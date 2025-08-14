# Star Wars Galaxies Steam Integration
Star Wars Galaxies Steam Integration is a simple wrapper application written in C# that automates the launch process for private SWG servers. It is designed to allow for seamless integration with Steam as a "Non-Steam Game," ensuring the Steam Overlay works correctly.

This has been tested and is confirmed to currently only work with the Star Wars Galaxies Restoration server, specifically the x64 version that is enabled in the user settings of the launcher.

# The Problem
Adding SWG launchers directly to Steam can be unreliable. Often, the Steam Overlay will attach to the launcher instead of the actual game client, or it won't work at all. This application solves that problem by managing the launch sequence.

# How It Works By Default (Star Wars Galaxies Restoration Only)
The application performs the following steps automatically:

Launches the Server Launcher: It starts Star Wars Galaxies Restoration's launcher (SWG Restoration.exe).

Waits: It pauses for 10 seconds. This gives the launcher time to check for updates and perform any necessary authentication (including auto-login).

Closes the Launcher: It terminates the launcher process to free up resources.

Launches the Game Client: It starts the main game client (SwgClient_r.exe) directly.

Waits for Exit: The wrapper runs silently in the background until you close the game, which allows Steam to accurately track your playtime.

# How to Manually Modify & Use For Your Star Wars Galaxies Server
Edit the File Paths: Open the .cs file in a text editor and change the launcherPath and gamePath variables to match the locations on your computer.

Compile the Application: Use the .NET Visual Studio SDK installed (you can download this seperately instead of downloading Visual Studio) to compile the code into an .exe file. 
Further Explanation to Compile: Move the modified .cs file to your preferred folder and copy the URL path (e.g., D:\Games\SWG\SWG Restoration\x64), then open up PowerShell. Type `cd "YOURPATHHERE"`, replacing YOURPATHHERE with the full path of the modified .cs file, but still keeping the quotation marks. Then, you will need to type `csc Star-Wars-Galaxies-Steam-Integration.cs`, which will compile your modified .cs file. It will make a new `Star-Wars-Galaxies-Steam-Integration.exe` file to which you can place anywhere.

You may have to add the PATH variable to the System Variables if csc does not work in terminal or PowerShell.

# Add to Steam:
In Steam, go to Library -> Add a Game -> Add a Non-Steam Game...

Browse to and select the compiled .exe file.

You can now launch the game from your Steam library!
