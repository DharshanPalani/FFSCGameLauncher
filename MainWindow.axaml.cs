using Avalonia.Controls;
using System;
using System.IO;
using System.Text.Json;

namespace GameLauncher
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnStartClicked(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            string configPath = ConfigHelper.GetConfigPath();
            GameConfig config;

            if (File.Exists(configPath))
            {
                string existingConfigJson = File.ReadAllText(configPath);
                config = JsonSerializer.Deserialize<GameConfig>(existingConfigJson) ?? throw new InvalidOperationException("Failed to parse config.");
                Console.WriteLine($"Existing config loaded: {existingConfigJson}");
            }
            else
            {
                config = new GameConfig
                {
                    GameInstallPath = @"C:\FriendlyFriendsStudentCouncil\",
                    GameVersion = "1.0.0"
                };

                string json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(configPath, json);
                Console.WriteLine("New config written to file.");
            }

            if (GamePathHelper.GamePathExists(config.GameInstallPath))
            {
                Console.WriteLine("Game path exists.");
            }
            else
            {
                Console.WriteLine("Game path does not exist. Creating it now.");
                GamePathHelper.CreateGamePath(config.GameInstallPath);
            }

            if (RunGameHelper.CheckGameExecutableExists(config.GameInstallPath))
            {
                Console.WriteLine("Game executable exists.");
                RunGameHelper.RunGameExecutable(config.GameInstallPath);
            }
            else
            {
                Console.WriteLine("Game executable does not exist. Please check your installation.");
            }

            Close();
        }
    }
}
