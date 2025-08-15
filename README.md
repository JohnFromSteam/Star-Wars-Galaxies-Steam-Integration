# Star Wars Galaxies Steam Integration

Star Wars Galaxies Steam Integration is a silent wrapper application for private SWG servers. It is designed for seamless integration with Steam as a "Non-Steam Game", ensuring the Steam Overlay works correctly by attaching to the game client after it starts your pathed SWG launcher and the SWG launcher closes (by itself when pressing 'Play' or by you manually closing it).

This application is now configurable via a simple text file (`config.ini`), allowing it to work with virtually any SWG server launcher.

# The Original Problem

Adding SWG launchers directly to Steam can be unreliable. The Steam Overlay will usually attach to your SWG launcher process, which usually closes after you click "Play", causing the overlay to disappear. This application solves that problem by managing the launch sequence, process monitoring for the configured SWG client name after the launcher closes, then staying alive until the SWG client finally closes.

# How It Works

The application runs silently and apart from the command terminal that pops up that indicates that it is running, it does all of the logic in the background to provide a seamless experience. It performs the following steps:

1.  **Reads Configuration:** It reads the `launcherPath` and `gameProcessName` from the `config.ini` file.
2.  **Launches the Server Launcher:** It starts the launcher executable as specified in the `config.ini` file (e.g., `D:\Games\SWG\SWG Restoration\SWG Restoration.exe`).
3.  **Waits for Launcher to Close:** The application waits patiently until you click "Play" and the launcher process closes itself (you may have to close the launcher manually if it does not do that automatically).
4.  **Finds the Game Client:** It immediately begins searching for the main game process as specified in the `config.ini` file (e.g., `SwgClient_r.exe`). It searches for the client each second, for 15 seconds. If it is not found after 15 seconds, the application closes.
5.  **Game is Found:** Once the game process is found, the wrapper stays alive.
6.  **Exits Automatically:** When you close the game, the wrapper detects this and automatically closes itself.

# How to Install and Use

### ⚠️ Important Note on Security Warnings
Your browser and Windows Defender will likely flag this application as unrecognized or potentially unsafe. **This is a false positive.**

This happens because the application is new and is not code-signed (a costly process for small developers).

If you are doubtful of the application, you can compile the application yourself via the source code in the main repo directory:

**Star-Wars-Galaxies-Steam-Integration/SWGSI/source-code/Star Wars Galaxies Steam Integration.cs**

If you want to see the VirusTotal checks for the URL and all the files, you can go to the links here:

**GitHub ZIP URL**:

https://www.virustotal.com/gui/url/dd3f155d90685c3e24eae9a10bf4ec41980de24d1ebb9b39e6cdd23b664cddae/detection

**config.ini**:

https://www.virustotal.com/gui/file/b5163d11f3be8921aa0e21386203fd20f751cfb22238113439ed660555a5848f/detection

**Star Wars Galaxies Steam Integration.deps.json**:

https://www.virustotal.com/gui/file/c63e72068a8462bc82b130ecb0de68075c5cf88fd4a98894db35be0968f87fe2/detection

**Star Wars Galaxies Steam Integration.dll**:

https://www.virustotal.com/gui/file/1fb73a8c0208f97508c8fb9741e74ecb330820b407435776f064c1507f57f06e/detection

**Star Wars Galaxies Steam Integration.exe**:

https://www.virustotal.com/gui/file/0c7bbcdcdadc933ebbae1459de4d07e93cb548568943631099dfb03b440e5c71/detection

**Star Wars Galaxies Steam Integration.runtimeconfig.json**:

https://www.virustotal.com/gui/file/3c0bf772b95c93363a0a14e5df78e4ec408c90f04cb7103205af97b2d276fab0/detection

### Step 1: Installation
1.  Go to the [Releases Page](https://github.com/JohnFromSteam/Star-Wars-Galaxies-Steam-Integration/releases) on the right side of this page.
2.  Download the `SWGSI.zip` file from the latest release.
3.  Your browser may flag the download as unsafe because it is a new application. **This is a false positive.**
    *   In Older Chrome Versions/Other Browsers: Go to the Downloads page and keep the file.
    *   In Windows Part I: Given that Windows Defender catches it first, open the Start menu and type *Virus, to pull up Virus & Threat Protection
    *   In Windows Part II: Click on Protection history and select the last Threat blocked. Click on Actions at the bottom right and recover the file.
4.  Extract all the files from the `.zip` into their own folder.
5.  Follow the configuration steps below.

### Step 2: Configuration
Open the `config.ini` file with any text editor (like Notepad). You will need to provide two values:

*   **`launcherPath`**: The full path to your server's launcher executable.
*   **`gameProcessName`**: The process name of the main game client **without** the `.exe` extension. This is important.

**Example of my config for my installation of SWG Restoration:**
```config.ini
# Configuration for the SWG Launcher Steam Integration Application

# Below is the template on how to set up the configuration file. Replace the paths with the actual paths on your system and replace the main game process name if it is different than 'SwgClient_r'.

# Path to the game launcher executable
launcherPath=D:\Games\SWG\SWG Restoration\SWG Restoration.exe

# The process name of the main game executable (excluding the .exe extension)
gameProcessName=SwgClient_r
```

### Step 3: Add to Steam
1.  In Steam, go to **Games (tip top tab in Steam) -> Add a Non-Steam Game to My Library...**
2.  Click **Browse...** and navigate to the folder where you placed the application.
3.  Select **`Star Wars Galaxies Steam Integration.exe`** and click **Add Selected Programs**.
4.  When you run the newly added program on Steam, you may need to click "More info" and then "Run anyway" since it is a new application and also unsigned. You should only need to do this once, thankfully.
5.  (Optional) You can rename the entry in your Steam library to anything and even add custom artwork (should be an upcoming Steam feature).

You can now launch the game from your Steam library and use the overlay!

# Source Code
The source code is provided in:

**Star-Wars-Galaxies-Steam-Integration/SWGSI/source-code/Star Wars Galaxies Steam Integration.cs**

I am in university and my language focus is Java, with Python/SQL/Front-End/Angular/Spring knowledge. Given that this is my first C# program, I do believe it could have been more efficient and have more logging/info to the terminal when it is running.

Aside from that, it does its job pretty well.

Thank you!

# Troubleshooting

### "The launcher never appears!"

This is the most common issue and is almost always caused by a permissions problem with your game's installation folder.

1.  **Check the Log File:** In the same folder as the application, a file named **`launcher_log.txt`** will be created. Open it to see the exact error message. An error like "The launcher process exited immediately" is a clear sign of a permissions issue.

2.  **The Solution (Recommended):** The error is caused by installing the game in a protected Windows folder (like `C:\Program Files` or `D:\Program Files`). To fix this permanently:
    *   Move your entire game installation folder (e.g., `SWG Restoration`) to a simple, non-protected path like **`C:\Games\`** or **`D:\MyGames\`**.
    *   Boot the SWG launcher to make sure things did not break. Your SWG launcher _may_ have issues detecting the files when you boot the launcher, as it may be searching for the original install path when you first installed it. If you have cannot change the install path inside your launcher, backup the **.tre** files from your game's directory as well as your **profiles** folder to somewhere else (that folder holds the settings for your character(s), uninstall the game via your launcher's uninstaller .exe, then place the backup'd files back into the game's folder and reinstall.
    *   After moving the folder, you **MUST** update the `launcherPath` in your `config.ini` file to point to the new location.
    *   This will solve the issue and allow the application to launch the game without needing administrator privileges.

3.  **The Other Solution (Not Recommended):** To fix this permanently via granting admin to `Star Wars Galaxies Steam Integration.exe`:
    *   Right-click `Star Wars Galaxies Steam Integration.exe` and select **Properties**.
    *   Go to the **Compatibility** tab and select **Run this program as an administrator**
    *   Press **Apply**, then **OK**.
    *   Launch the `Star Wars Galaxies Steam Integration.exe` to make sure this changed worked. You should receive a pop-up to allow admin access.

# Further Issues
If you have any further issues, let's try to figure it out together! Go to the **[Issues](https://github.com/JohnFromSteam/Star-Wars-Galaxies-Steam-Integration/issues)** section and fill out a report.
