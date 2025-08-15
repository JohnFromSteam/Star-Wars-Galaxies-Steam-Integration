// Star Wars Galaxies Steam Integration Launcher
// This application is designed to launch the Star Wars Galaxies game through a specified launcher,
using System.Diagnostics;
using System.Reflection;

public class SwgLauncherSteamApp
{
    // Static path for the log file
    private static string logFilePath = Path.Combine(AppContext.BaseDirectory, "launcher_log.txt");

    public static void Main()
    {
        try
        {
            // Gets the absolute path to the config.ini file.
            string exePath = Assembly.GetExecutingAssembly().Location;
            string exeDirectory = Path.GetDirectoryName(exePath);
            string configPath = Path.Combine(exeDirectory, "config.ini");

            var config = LoadConfiguration(configPath);
            if (config == null) return;

            // Reads both values from the config file
            config.TryGetValue("launcherPath", out string launcherPath);
            config.TryGetValue("gameProcessName", out string gameProcessName);

            // Checks if the launcherPath is null or empty
            if (string.IsNullOrEmpty(launcherPath) || string.IsNullOrEmpty(gameProcessName))
            {
                Log("FATAL: 'launcherPath' or 'gameProcessName' is missing from config.ini.");
                return;
            }

            // Determines the correct working directory from the launcher's path
            string launcherDirectory = Path.GetDirectoryName(launcherPath);

            // Configures the launcher process
            ProcessStartInfo launcherStartInfo = new ProcessStartInfo
            {
                FileName = launcherPath,
                WorkingDirectory = launcherDirectory,
                UseShellExecute = true
            };

            Process launcher = Process.Start(launcherStartInfo);

            // Shows an error message if the launcher process could not be started
            if (launcher == null)
            {
                Log("ERROR: Process.Start() returned null. The launcher process could not be created.");
                return;
            }

            // Gives the launcher 2 seconds to start, otherwise shows a message
            if (launcher.WaitForExit(2000))
            {
                Log($"ERROR: The launcher process exited immediately with code {launcher.ExitCode}. This is a strong indicator of a permissions issue. Try moving the game to a non-protected folder like C:\\Games\\.");
                return;
            }

            // If the launcher is still running, waits for it to close normally.
            Log("Launcher is running. Waiting for it to close...");
            launcher.WaitForExit();
            Log("Launcher has closed. Searching for game process...");

            // Finds the game process by its name
            Process gameProcess = null;

            // Polls each second for 15 seconds to give the game time to start
            for (int i = 0; i < 15; i++)
            {
                var processes = Process.GetProcessesByName(gameProcessName);
                if (processes.Length > 0)
                {
                    gameProcess = processes[0];
                    Log($"Found game process: {gameProcess.ProcessName} (ID: {gameProcess.Id})");
                    break; // Exits the loop since we found it
                }

                // Waits for one second before checking again
                Thread.Sleep(1000);
            }

            // Waits for game to exit or hanedles the case where the game process is not found
            if (gameProcess != null)
            {
                Log("Waiting for game process to exit...");
                gameProcess.WaitForExit();
                Log("Game has closed. Exiting.");
            }
            else
            {
                Log($"ERROR: Could not find game process '{gameProcessName}.exe' after 15 seconds.");
            }
        }
        catch (Exception ex)
        {
            Log($"FATAL EXCEPTION: {ex.Message}\n{ex.StackTrace}");
        }
    }

    private static Dictionary<string, string> LoadConfiguration(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Log($"FATAL: Configuration file not found at '{filePath}'");
            return null;
        }

        // Initializes the Dictionary
        var config = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
        try
        {
            // Reads the file line by line
            foreach (var line in File.ReadAllLines(filePath))
            {
                // Filters beginning lines that are null/whitespace/#/;
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#") || line.StartsWith(";"))
                    continue;

                // Splits the line
                var parts = line.Split(new char[] { '=' }, 2);

                // Populates the Dictionary if key/value is correct
                if (parts.Length == 2)
                {
                    config[parts[0].Trim()] = parts[1].Trim().Trim('"');
                }
            }
            return config;
        }
        catch (Exception ex)
        {
            Log($"FATAL: Could not read or parse config file. Error: {ex.Message}");
            return null;
        }
    }
    private static void Log(string message)
    {
        try
        {
            // Prepends a timestamp to every log message for clarity
            string formattedMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}{Environment.NewLine}";
            File.AppendAllText(logFilePath, formattedMessage);
        }
        catch
        {
            // If logging fails, there's nothing we can do, so we fail silently.
        }
    }
}