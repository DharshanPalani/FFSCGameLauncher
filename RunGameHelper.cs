using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace GameLauncher
{
    public static class RunGameHelper
    {
        public static void RunGameExecutable()
        {
            string configPath = ConfigHelper.GetConfigPath();

            if (!File.Exists(configPath))
            {
                return;
            }

            try
            {
                string configJson = File.ReadAllText(configPath);
                var config = JsonSerializer.Deserialize<GameConfig>(configJson);

                if (config == null || string.IsNullOrEmpty(config.GameInstallPath))
                {
                    // Handle the case where the config is invalid or missing
                    return;
                }

                string folderPath = Path.GetDirectoryName(config.GameInstallPath)!;
                string targetExePath = Path.Combine(folderPath, "Friendly Friends Student Council.exe");

                if (!File.Exists(targetExePath))
                {
                    return;
                }

                Process.Start(new ProcessStartInfo
                {
                    FileName = targetExePath,
                    UseShellExecute = true
                });

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading config or launching game: {ex.Message}");
            }
        }
    }
}
