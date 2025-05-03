using System;
using System.IO;

public static class ConfigHelper
{
    public static string GetConfigPath()
    {
        var appName = "FriendlyFriendsStudentCouncil";
        string basePath;

        if (OperatingSystem.IsWindows())
        {
            // Windows has APPDATA so this is for Windows.
            basePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }
        else
        {
            // This is for Linux and MacOS.
            basePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".config");
        }

        string configDir = Path.Combine(basePath, appName);
        Directory.CreateDirectory(configDir); // Ensure directory exists
        return Path.Combine(configDir, "config.json");
    }
}
