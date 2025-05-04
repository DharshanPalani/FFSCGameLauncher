using System;
using System.Diagnostics;
using System.IO;

namespace GameLauncher
{
    public static class RunGameHelper
    {
        public static void RunGameExecutable(string gamePath)
        {
            string exePath = Path.Combine(gamePath, "Friendly Friends Student Council.exe");

            if (!File.Exists(exePath))
            {
                Console.WriteLine("Executable not found at: " + exePath);
                return;
            }

            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = exePath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error launching game: {ex.Message}");
            }
        }

        public static bool CheckGameExecutableExists(string gamePath)
        {
            string exePath = Path.Combine(gamePath, "Friendly Friends Student Council.exe");
            return File.Exists(exePath);
        }
    }
}
